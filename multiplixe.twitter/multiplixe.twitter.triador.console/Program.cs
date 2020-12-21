using multiplixe.comum.exceptions;
using multiplixe.comum.helper;
using multiplixe.enfileirador.client;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;
using registrador = multiplixe.registrador_de_eventos.client.twitter;
using twitterdto = multiplixe.twitter.dto;
using reacaotriador = multiplixe.twitter.reacao.triador;
using multiplixe.usuarios.client;
using multiplixe.comum.triador;

namespace multiplixe.twitter.triador.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                            .AddSingleton<EnfileiradorClient>()
                            .AddTransient<TriadorService>()
                            .AddTransient<TriadorService<twitterdto.eventos.EventoReacao>>()
                            .AddScoped(typeof(coreinterfaces.triador.IRegistradorEventoTriagem<twitterdto.eventos.EventoReacao>), typeof(reacaotriador.Registrador))
                            .AddScoped(typeof(coreinterfaces.IRegistradorEventosConsultas<twitterdto.eventos.EventoReacao>), typeof(reacaotriador.Registrador))
                            .AddScoped(typeof(coreinterfaces.triador.IAvaliadorDeEvento<twitterdto.eventos.EventoReacao>), typeof(reacaotriador.Avaliador))
                            .AddScoped(typeof(coreinterfaces.triador.IValidadorDeEvento<twitterdto.eventos.EventoReacao>), typeof(reacaotriador.Validador))
                            .AddScoped(typeof(coreinterfaces.triador.IIdentificadorUsuario<twitterdto.eventos.EventoReacao>), typeof(reacaotriador.IdentificadorUsuario))
                            .AddScoped(typeof(coreinterfaces.triador.ITriador<twitterdto.eventos.EventoReacao>), typeof(reacaotriador.Triador))
                            .AddTransient<PerfilClient>()
                            .AddTransient<registrador.Client>()
                            .BuildServiceProvider();

            var triadorTwitterService = serviceProvider.GetService<TriadorService>();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.TriadorTwitter();

            var factory = new ConnectionFactory() { HostName = filaConfig.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var filaDeclarada = channel.QueueDeclare(queue: filaConfig.Nome,
                                     durable: filaConfig.Durable,
                                     exclusive: filaConfig.Exclusive,
                                     autoDelete: filaConfig.AutoDelete,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume(queue: filaConfig.Nome,
                     autoAck: filaConfig.AutoAck,
                     consumer: consumer);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                        var envelope = DeserializadorHelper.Deserializar<comum_dto.EnvelopeEvento<twitterdto.eventos.Evento>>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Empresa: {0}", envelope.EmpresaId);
                        Console.WriteLine("DataEvento: {0}", envelope.DataEvento);
                        Console.WriteLine("Data: {0}", DateTimeHelper.Now());

                        triadorTwitterService.ProcessarEnvelope(envelope);

                        Console.WriteLine("Processou");

                        if (!filaConfig.AutoAck)
                        {
                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                    }
                    catch (EventoInvalidoException eventoInvalidoException)
                    {
                        //##TODO logar
                        Console.WriteLine("*************************************************************");
                        Console.WriteLine("Evento inválido");
                        Console.WriteLine(DateTimeHelper.Now());
                        Console.WriteLine(Encoding.UTF8.GetString(ea.Body.ToArray()));
                        Console.WriteLine("*************************************************************");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("*************************************************************");
                        Console.WriteLine("Erro");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("*************************************************************");
                    }

                };

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine("*************************************************");
                Console.WriteLine($"Triador Twitter aguardando - {DateTimeHelper.Now()}");
                Console.WriteLine("*************************************************");
                Console.WriteLine("");
                Console.ResetColor();

                Console.ReadLine();
            }
        }
    }
}

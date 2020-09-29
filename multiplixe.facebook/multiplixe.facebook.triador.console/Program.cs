using Microsoft.Extensions.DependencyInjection;
using multiplixe.comum.triador;
using multiplixe.enfileirador.client;
using multiplixe.facebook.dto.eventos;
using multiplixe.usuarios.client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using comum_dto = multiplixe.comum.dto;
using coreexceptions = multiplixe.comum.exceptions;
using corehelper = multiplixe.comum.helper;
using coreinterfaces = multiplixe.comum.interfaces;
using reacaotriador = multiplixe.facebook.reacao.triador;
using registrador = multiplixe.registrador_de_eventos.client.facebook;

namespace multiplixe.facebook.triador.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                            .AddTransient<TriadorService<Evento>>()
                            .AddTransient<EnfileiradorClient>()
                            .AddTransient<registrador.Client>()
                            .AddTransient<PerfilClient>()
                            .AddScoped(typeof(coreinterfaces.triador.IRegistradorEventoTriagem<Evento>), typeof(reacaotriador.RegistradorReacao))
                            .AddScoped(typeof(coreinterfaces.IRegistradorEventosConsultas<Evento>), typeof(reacaotriador.RegistradorReacao))
                            .AddScoped(typeof(reacaotriador.IAvaliadorCurtida), typeof(reacaotriador.AvaliadorCurtida))
                            .AddScoped(typeof(reacaotriador.IAvaliadorDescurtida), typeof(reacaotriador.AvaliadorDescurtida))
                            .AddScoped(typeof(coreinterfaces.triador.ITriador<Evento>), typeof(Triador))
                            .AddScoped(typeof(coreinterfaces.triador.IIdentificadorUsuario<Evento>), typeof(IdentificadorUsuario))
                            .AddScoped(typeof(coreinterfaces.triador.IValidadorDeEvento<Evento>), typeof(Validador))
                            .BuildServiceProvider();

            var triadorService = serviceProvider.GetService<TriadorService<Evento>>();
            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.TriadorFacebook();

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

                        var envelope = corehelper.DeserializadorHelper.Deserializar<comum_dto.EnvelopeEvento<Evento>>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Empresa: {0}", envelope.EmpresaId);
                        Console.WriteLine("DataEvento: {0}", envelope.DataEvento);
                        Console.WriteLine("Data: {0}", corehelper.DateTimeHelper.Now());

                        triadorService.ProcessarEnvelope(envelope);

                        Console.WriteLine("Processou");

                        if (!filaConfig.AutoAck)
                        {
                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                    }
                    catch (coreexceptions.EventoInvalidoException eventoInvalidoException)
                    {
                        //##TODO logar
                        Console.WriteLine("*************************************************************");
                        Console.WriteLine("Evento inválido");
                        Console.WriteLine(corehelper.DateTimeHelper.Now());
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

                Console.WriteLine("Triador Facebook aguardando...");
                Console.ReadLine();
            }
        }
    }
}

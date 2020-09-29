using multiplixe.enfileirador.client;
using Microsoft.Extensions.DependencyInjection;
using multiplixe.twitch.dto.eventos;
using pingtriador = multiplixe.twitch.ping.triador;
using System;
using coreinterfaces = multiplixe.comum.interfaces;
using multiplixe.usuarios.client;
using registrador = multiplixe.registrador_de_eventos.client.twitch;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using multiplixe.comum.dto;
using multiplixe.comum.helper;
using multiplixe.comum.triador;
using multiplixe.comum.exceptions;

namespace multiplixe.twitch.triador.console
{
    class Program
    {
        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
                     .AddSingleton<EnfileiradorClient>()
                     .AddTransient<TriadorService<Evento>>()
                     .AddScoped(typeof(coreinterfaces.triador.IRegistradorEventoTriagem<EventoPing>), typeof(pingtriador.Registrador))
                     .AddScoped(typeof(coreinterfaces.triador.IValidadorDeEvento<Evento>), typeof(Validador))
                     .AddScoped(typeof(coreinterfaces.triador.IAvaliadorDeEvento<EventoPing>), typeof(pingtriador.Avaliador))
                     .AddScoped(typeof(coreinterfaces.triador.IIdentificadorUsuario<Evento>), typeof(IdentificadorUsuario))
                     .AddScoped(typeof(coreinterfaces.triador.ITriador<Evento>), typeof(Triador))
                     .AddTransient<PerfilClient>()
                     .AddTransient<registrador.Client>()
                     .BuildServiceProvider();

            var triadorService = serviceProvider.GetService<TriadorService<Evento>>();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.TriadorTwitch();

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

                        var envelope = DeserializadorHelper.Deserializar<EnvelopeEvento<Evento>>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Empresa: {0}", envelope.EmpresaId);
                        Console.WriteLine("DataEvento: {0}", envelope.DataEvento);
                        Console.WriteLine("Data: {0}", DateTimeHelper.Now());

                        Console.WriteLine("Atual: {0}", envelope.Evento.Ping.Atual);
                        Console.WriteLine("Ultimo: {0}", envelope.Evento.Ping.Ultimo);
                        Console.WriteLine("FrequenciaMinutos: {0}", envelope.Evento.Ping.FrequenciaMinutos);
                        Console.WriteLine("ToleranciaSegundos: {0}", envelope.Evento.Ping.ToleranciaSegundos);
                        Console.WriteLine("PausaMilissegundos: {0}", envelope.Evento.Ping.PausaMilissegundos);
                        Console.WriteLine("TotalSeconds: {0}", (envelope.Evento.Ping.Atual - envelope.Evento.Ping.Ultimo).TotalSeconds);

                        triadorService.ProcessarEnvelope(envelope);

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

                Console.WriteLine("Triador Twitch aguardando...");
                Console.ReadLine();
            }
        }
    }
}
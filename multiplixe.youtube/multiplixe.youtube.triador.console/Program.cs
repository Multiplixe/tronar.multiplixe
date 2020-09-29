using multiplixe.enfileirador.client;
using multiplixe.usuarios.client;
using Microsoft.Extensions.DependencyInjection;
using livehashtag = multiplixe.youtube.livehashtag.triador;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using coredto = multiplixe.comum.dto;
using coreexceptions = multiplixe.comum.exceptions;
using corehelper = multiplixe.comum.helper;
using coreinterfaces = multiplixe.comum.interfaces;
using multiplixe.comum.triador;

namespace multiplixe.youtube.triador.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                                        .AddTransient<TriadorService<dto.eventos.Evento>>()
                                        .AddTransient<EnfileiradorClient>()
                                        .AddTransient<PerfilClient>()
                                        .AddTransient(typeof(coreinterfaces.triador.IIdentificadorUsuario<dto.eventos.Evento>), typeof(IdentificadorUsuario))
                                        .AddTransient(typeof(coreinterfaces.triador.ITriador<dto.eventos.Evento>), typeof(Triador))
                                        .AddTransient(typeof(coreinterfaces.triador.IValidadorDeEvento<dto.eventos.Evento>), typeof(Validador))
                                        .BuildServiceProvider();


            var triadorService = serviceProvider.GetService<TriadorService<dto.eventos.Evento>>();
            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.TriadorYoutube();

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

                        var envelope = corehelper.DeserializadorHelper.Deserializar<coredto.EnvelopeEvento<dto.eventos.Evento>>(json);

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
                    catch (coreexceptions.EventoInvalidoException)
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

                Console.WriteLine("Triador Youtube aguardando...");
                Console.ReadLine();
            }
        }
    }
}

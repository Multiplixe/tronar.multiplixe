using multiplixe.enfileirador.client;
using Microsoft.Extensions.DependencyInjection;
using multiplixe.twitch.dto.eventos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using comum_dto = multiplixe.comum.dto;
using corehelper = multiplixe.comum.helper;
using multiplixe.comum.interfaces.pontuador;
using multiplixe.comum.pontuador;

namespace multiplixe.twitch.ping.pontuador.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                             .AddTransient<EnfileiradorClient>()
                             .AddScoped(typeof(IPontuador<EventoPing>), typeof(Pontuador))
                             .AddScoped(typeof(IPontuadorService<>), typeof(PontuadorService<>))
                             .BuildServiceProvider();

            var pontuadorService = serviceProvider.GetService<IPontuadorService<EventoPing>>();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.PontuadorPingTwitch();

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

                        var envelope = corehelper.DeserializadorHelper.Deserializar<comum_dto.EnvelopeEvento<EventoPing>>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Empresa: {0}", envelope.EmpresaId);
                        Console.WriteLine("Usuario: {0}", envelope.UsuarioId);
                        Console.WriteLine("Data: {0}", corehelper.DateTimeHelper.Now());

                        pontuadorService.ProcessarEvento(envelope);

                        Console.WriteLine("Processou");

                        if (!filaConfig.AutoAck)
                        {
                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("*************************************************************");
                        Console.WriteLine("Erro");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("*************************************************************");
                    }
                };

                Console.WriteLine("Pontuador Ping Twitch aguardando...");
                Console.ReadLine();
            }
        }
    }
}

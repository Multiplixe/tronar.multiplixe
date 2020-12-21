using Microsoft.Extensions.DependencyInjection;
using multiplixe.twitter.dto.eventos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using coreinterfaces = multiplixe.comum.interfaces;
using corehelper = multiplixe.comum.helper;
using System.Text;
using comum_dto = multiplixe.comum.dto;
using multiplixe.enfileirador.client;
using multiplixe.comum.pontuador;
using multiplixe.comum.helper;

namespace multiplixe.twitter.reacao.pontuador.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                             .AddTransient<EnfileiradorClient>()
                             .AddScoped(typeof(coreinterfaces.pontuador.IPontuador<EventoReacao>), typeof(Pontuador))
                             .AddScoped(typeof(coreinterfaces.pontuador.IPontuadorService<>), typeof(PontuadorService<>))
                             .BuildServiceProvider();

            var calculoService = serviceProvider.GetService<coreinterfaces.pontuador.IPontuadorService<EventoReacao>>();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.PontuadorReacaoTwitter();

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

                        var envelope = corehelper.DeserializadorHelper.Deserializar<comum_dto.EnvelopeEvento<EventoReacao>>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Empresa: {0}", envelope.EmpresaId);
                        Console.WriteLine("Usuario: {0}", envelope.UsuarioId);
                        Console.WriteLine("Data: {0}", corehelper.DateTimeHelper.Now());

                        calculoService.ProcessarEvento(envelope);

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

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine("*************************************************");
                Console.WriteLine($"Pontuador Reação Twitter aguardando - {DateTimeHelper.Now()}");
                Console.WriteLine("*************************************************");
                Console.WriteLine("");
                Console.ResetColor();

                Console.ReadLine();
            }
        }
    }
}

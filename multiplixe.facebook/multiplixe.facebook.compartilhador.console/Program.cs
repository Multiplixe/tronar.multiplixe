using Microsoft.Extensions.DependencyInjection;
using multiplixe.enfileirador.client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

using corehelper = multiplixe.comum.helper;
using comum_dto = multiplixe.comum.dto;
using multiplixe.usuarios.client;
using multiplixe.compartilhador.client;

namespace multiplixe.facebook.compartilhador.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                            .AddTransient<Servico>()
                            .AddTransient<EnfileiradorClient>()
                            .AddTransient<PerfilClient>()
                            .AddTransient<CompartilhamentoClient>()
                            .AddTransient<PostClient>()
                            .BuildServiceProvider();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();
            var servicoCompartilhador = serviceProvider.GetService<Servico>();

            var filaConfig = enfileiradorClient.CompartilhadorFacebook();

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

                        var o = corehelper.DeserializadorHelper.Deserializar<object>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("JSON: {0}", json);
                        Console.WriteLine("Data: {0}", corehelper.DateTimeHelper.Now());

                        servicoCompartilhador.Compartilhar(o);

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

                var foregroundColor = Console.ForegroundColor;
                var backgroundColor = Console.BackgroundColor;

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine($"* Facebook compartilhador aguardando - {corehelper.DateTimeHelper.Now()}  ");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("");

                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;


                Console.ReadLine();
            }
        }
    }
}

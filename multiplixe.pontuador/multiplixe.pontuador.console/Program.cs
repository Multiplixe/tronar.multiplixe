using multiplixe.comum.dapper;
using multiplixe.comum.helper;
using multiplixe.enfileirador.client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using coredto = multiplixe.comum.dto;

namespace multiplixe.pontuador.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(
                             path: "appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                       .Build();

            var serviceProvider = new ServiceCollection()
                                        .AddSingleton<IConfiguration>(configuration)
                                        .AddTransient<DapperHelper>()
                                        .AddTransient<EnfileiradorClient>()
                                        .AddTransient<Repositorio>()
                                        .AddTransient<Servico>()
                                        .BuildServiceProvider();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();
            var servico = serviceProvider.GetService<Servico>();

            var filaConfig = enfileiradorClient.Pontuador();

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
                        var body = ea.Body;
                        var json = Encoding.UTF8.GetString(body.ToArray());

                        var ponto = DeserializadorHelper.Deserializar<coredto.Ponto>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Evento: {0}", ponto.EventoId);
                        Console.WriteLine("Empresa: {0}", ponto.EmpresaId);
                        Console.WriteLine("Post: {0}", ponto.PostId);
                        Console.WriteLine("Data: {0}", DateTimeHelper.Now());

                        servico.RegistrarExtrato(ponto);

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

                Console.WriteLine("Pontuador extrato aguardando...");
                Console.ReadLine();
            }
        }
    }
}
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

namespace multiplixe.consolidador.console
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
                                        .AddTransient<eventos.Repositorio>()
                                        .AddTransient<eventos.Servico>()
                                        .AddTransient<pontuacao.Repositorio>()
                                        .AddTransient<pontuacao.Servico>()
                                        .AddTransient<saldo.Repositorio>()
                                        .AddTransient<saldo.Servico>()
                                        .AddTransient<Repositorio>()
                                        .AddTransient<Servico>()
                                        .BuildServiceProvider();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();
            
            var servico = serviceProvider.GetService<Servico>();

            var filaConfig = enfileiradorClient.Consolidador();

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
                    var body = ea.Body;
                    var json = Encoding.UTF8.GetString(body.ToArray());

                    var ponto = DeserializadorHelper.Deserializar<coredto.Ponto>(json);

                    try
                    {
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Evento: {0}", ponto.EventoId);
                        Console.WriteLine("Empresa: {0}", ponto.EmpresaId);
                        Console.WriteLine("UsuarioId: {0}", ponto.UsuarioId);
                        Console.WriteLine("Post: {0}", ponto.PostId);
                        Console.WriteLine("Data: {0}", DateTimeHelper.Now());

                        servico.Processar(ponto);

                        Console.WriteLine("Processou");

                        if (!filaConfig.AutoAck)
                        {
                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                    }
                    catch (Exception ex)
                    {
                        servico.Rollback(ponto);

                        Console.WriteLine("*************************************************************");
                        Console.WriteLine("Erro");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("*************************************************************");
                    }

                };

                Console.WriteLine("Consolidador aguardando...");
                Console.ReadLine();
            }
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using multiplixe.central_rtdb.client;
using multiplixe.comum.helper;
using multiplixe.empresas.client;
using multiplixe.enfileirador.client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.avatar.console
{
    class Program
    {
        private static AppSettings appSettings { get; set; }
        private static ServiceProvider serviceProvider { get; set; }

        static void Main(string[] args)
        {
            Configuracoes();

            var enfileiradorClient = new EnfileiradorClient();

            var servico = serviceProvider.GetService<services.Servico>();
            
            var filaConfig = enfileiradorClient.Avatar();

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

                        var avatarParaProcessar = DeserializadorHelper.Deserializar<dto.AvatarParaProcessar>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("UsuarioId: {0}", avatarParaProcessar.UsuarioId);
                        Console.WriteLine("EmpresaId: {0}", avatarParaProcessar.EmpresaId);
                        Console.WriteLine("Imagem: {0}", avatarParaProcessar.Avatar.Imagem);
                        Console.WriteLine("Caminho: {0}", avatarParaProcessar.Caminho);
                        Console.WriteLine("Data: {0}", DateTimeHelper.Now());

                        servico.Processar(avatarParaProcessar);

                        Console.WriteLine("Processou");

                        if (!filaConfig.AutoAck)
                        {
                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                    }
                    catch (Exception ex)
                    {
                        //channel.BasicNack(ea.DeliveryTag, false, true);

                        Console.WriteLine("*************************************************************");
                        Console.WriteLine("Erro");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("*************************************************************");
                    }

                };

                var foregroundColor = Console.ForegroundColor;
                var backgroundColor = Console.BackgroundColor;

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"* Avatar aguardando - {DateTimeHelper.Now()}");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("");

                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;

                Console.ReadLine();
            }
        }

        private static void Configuracoes()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                 path: "appsettings.json",
                 optional: false,
                 reloadOnChange: true)
           .Build();

            appSettings = configuration.GetSection("Configuracoes").Get<AppSettings>();

            serviceProvider = new ServiceCollection()
                                        .AddSingleton<AppSettings>(appSettings)
                                        .AddTransient<EmpresaClient>()
                                        .AddTransient<RTDBAtividadeClient>()
                                        .AddTransient<services.Servico>()
                                        .AddTransient<services.PathHelper>()
                                        .AddTransient<services.Firebase>()
                                        .AddTransient<services.ProcessadorDeImagem>()
                                        .BuildServiceProvider();

        }


    }
}

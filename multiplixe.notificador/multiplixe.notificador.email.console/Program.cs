using corehelper = multiplixe.comum.helper;
using coredto = multiplixe.comum.dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Options;
using multiplixe.empresas.client;
using multiplixe.enfileirador.client;

namespace multiplixe.notificador.email.console
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

            var smtpSettings = new SmtpSettings();
            new ConfigureFromConfigurationOptions<SmtpSettings>(configuration.GetSection("Smtp")).Configure(smtpSettings);

            var serviceProvider = new ServiceCollection()
                                        .AddTransient<Notificador>()
                                        .AddTransient<SmtpService>()
                                        .AddTransient<EmpresaClient>()
                                        .AddTransient<EnfileiradorClient>()
                                        .AddSingleton<SmtpSettings>(smtpSettings)
                                        .BuildServiceProvider();

            var service = serviceProvider.GetService<Notificador>();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.NotificadorEmail();

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

                        var notificacao = corehelper.DeserializadorHelper.Deserializar<coredto.Notificacao>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Titulo: {0}", notificacao.Titulo);
                        Console.WriteLine("Email: {0}", notificacao.Destinatario);
                        Console.WriteLine("Data: {0}", corehelper.DateTimeHelper.Now());

                        service.Enviar(notificacao);

                        Console.WriteLine("Enviou");

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

                Console.WriteLine("Notificador e-mail aguardando...");
                Console.ReadLine();
            }
        }
    }
}

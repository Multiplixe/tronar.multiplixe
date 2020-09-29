using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using multiplixe.classificador.client;
using multiplixe.empresas.client;
using multiplixe.enfileirador.client;
using multiplixe.usuarios.client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using coredto = multiplixe.comum.dto;
using corehelper = multiplixe.comum.helper;

namespace multiplixe.notificador.twitch.pubsub.console
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

            var twitchSettings = new TwitchSettings();
            new ConfigureFromConfigurationOptions<TwitchSettings>(configuration.GetSection("TwitchSettings")).Configure(twitchSettings);

            var serviceProvider = new ServiceCollection()
                                        .AddTransient<Whisper>()
                                        .AddTransient<GeradorJWT>()
                                        .AddTransient<EnfileiradorClient>()
                                        .AddTransient<PerfilClient>()
                                        .AddTransient<EmpresaClient>()
                                        .AddTransient<ClassificadorClient>()
                                        .AddSingleton<TwitchSettings>(twitchSettings)
                                        .BuildServiceProvider();

            var service = serviceProvider.GetService<Whisper>();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.NotificadorTwitchPubSub();

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

                        var usuarioParaProcessar = corehelper.DeserializadorHelper.Deserializar<coredto.UsuarioParaProcessar>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("UsuarioId: {0}", usuarioParaProcessar.UsuarioId);
                        Console.WriteLine("Data: {0}", corehelper.DateTimeHelper.Now());

                        service.Publicar(usuarioParaProcessar);

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

                Console.WriteLine("Notificador Twitch Pub/Sub aguardando...");
                Console.ReadLine();
            }
        }
    }
}

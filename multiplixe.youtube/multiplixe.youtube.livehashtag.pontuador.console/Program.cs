using multiplixe.enfileirador.client;
using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using coreinterfaces = multiplixe.comum.interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using corehelper = multiplixe.comum.helper;
using coredto = multiplixe.comum.dto;
using multiplixe.comum.pontuador;

namespace multiplixe.youtube.livehashtag.pontuador.console
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
                             .AddTransient<EnfileiradorClient>()
                             .AddScoped(typeof(coreinterfaces.pontuador.IPontuador<dto.eventos.LiveHashtag>), typeof(Pontuador))
                             .AddScoped(typeof(coreinterfaces.pontuador.IPontuadorService<>), typeof(PontuadorService<>))
                             .BuildServiceProvider();

            var calculoService = serviceProvider.GetService<coreinterfaces.pontuador.IPontuadorService<dto.eventos.LiveHashtag>>();

            var enfileiradorClient = serviceProvider.GetService<EnfileiradorClient>();

            var filaConfig = enfileiradorClient.PontuadorLiveHashtagYoutube();

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

                        var envelope = corehelper.DeserializadorHelper.Deserializar<coredto.EnvelopeEvento<dto.eventos.LiveHashtag>>(json);

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Empresa: {0}", envelope.EmpresaId);
                        Console.WriteLine("Usuario: {0}", envelope.UsuarioId);
                        Console.WriteLine("Data: {0}", corehelper.DateTimeHelper.Now());

                        calculoService.ProcessarEvento(envelope);

                        Console.WriteLine("Processou");

                        if (!filaConfig.AutoAck)
                        {
                          //  channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
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

                Console.WriteLine("Pontuador Youtube Live Hashtag aguardando...");
                Console.ReadLine();
            }
        }
    }
}

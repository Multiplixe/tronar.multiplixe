using multiplixe.enfileirador.client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace multiplixe.youtube.livehashtag.console
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

            var appSettings = new AppSettings();
            new ConfigureFromConfigurationOptions<AppSettings>(configuration.GetSection("settings")).Configure(appSettings);

            var serviceProvider = new ServiceCollection()
                            .AddSingleton<AppSettings>(appSettings)
                            .AddSingleton<oauth2.Servico>()
                            .AddSingleton<livebroadcasts.Servico>()
                            .AddSingleton<messages.Servico>()
                            .AddSingleton<EnfileiradorClient>()
                            .BuildServiceProvider();

            _ = serviceProvider.GetService<oauth2.Servico>();
            var livebroadcastsServico = serviceProvider.GetService<livebroadcasts.Servico>();
            var messagesServico = serviceProvider.GetService<messages.Servico>();

            try
            {


                var hashtags = new List<string> { "#falkol", "#goblue" };

                var liveResponse = livebroadcastsServico.ObterLive();

                if (liveResponse.Success)
                {
                    var live = liveResponse.Item;

                    Console.WriteLine("Início da live -> {0}", live.Snippet.Title);

                    var liveChatId = live.Snippet.LiveChatId;

                    messagesServico.Processar(liveChatId, hashtags);

                    Console.WriteLine("Fim da live -> {0}", live.Snippet.Title);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO {0}", ex.Message);
                //## TODO log
            }

        }
    }
}

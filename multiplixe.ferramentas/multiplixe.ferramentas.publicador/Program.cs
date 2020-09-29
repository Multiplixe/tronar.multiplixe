using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using multiplixe.ferramentas.comum;
using multiplixe.ferramentas.comum.commands;
using System;
using System.IO;
using System.Security.Permissions;

namespace multipixel.ferramentas.publicador
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void Run()
        {

            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(
                             path: "appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                       .Build();

            var appSettings = new AppSettings();
            new ConfigureFromConfigurationOptions<AppSettings>(configuration.GetSection("Settings")).Configure(appSettings);

            using (var watcher = new FileSystemWatcher())
            {
                watcher.Path = appSettings.Path; // @"C:\\inetpub\\wwwroot\\tronar.multiplixe\\multiplixe.comum\\multiplixe.comum.enums\\bin\\Release";

                watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;

                watcher.Filter = "*.nupkg";

                watcher.EnableRaisingEvents = true;

                watcher.IncludeSubdirectories = true;

                watcher.Created += (object sender, FileSystemEventArgs e) =>
                {
                    if (e.FullPath.ToLower().Contains("release"))
                    {
                        Console.WriteLine(">> {0}", e.FullPath);
                        var command = new PublishNugetPackageCommand(e.FullPath, appSettings.Token);
                        ExecuteCommand.Execute(command);
                    }
                    else
                    {
                        Console.WriteLine("Pacote não esta em modo RELEASE");
                    }
                };

                Console.WriteLine($"Ouvindo pasta {appSettings.Path} ('q' pra sair)");
                while (Console.Read() != 'q') ;
            }
        }
    }
}

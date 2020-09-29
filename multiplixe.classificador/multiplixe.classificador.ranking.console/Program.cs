using multiplixe.comum.dapper;
using multiplixe.comum.helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using multiplixe.central_rtdb.client;
using multiplixe.empresas.client;
using System;
using System.IO;

namespace multiplixe.classificador.ranking.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ranking iniciando... {0}", DateTimeHelper.Now());

            var configuration = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile(
                               path: "appsettings.json",
                               optional: false,
                               reloadOnChange: true)
                         .Build();

            var parametros = ObterParametros(configuration);

            var serviceProvider = new ServiceCollection()
                                .AddSingleton<IConfiguration>(configuration)
                                .AddSingleton<ranking.Parametros>(parametros)
                                .AddTransient<EmpresaClient>()
                                .AddTransient<RTDBAtividadeComumClient>()
                                .AddTransient<DapperHelper>()
                                .AddTransient<ranking.Servico>()
                                .AddTransient<ranking.Repositorio>()
                                .BuildServiceProvider();

            var servico = serviceProvider.GetService<ranking.Servico>();

            servico.Processar();
        }

        private static ranking.Parametros ObterParametros(IConfigurationRoot configuration)
        {
            var parametrosConfig = configuration.GetSection(nameof(ranking.Parametros));

            var parametros = new ranking.Parametros
            {
                MenorPosicaoExibida = int.Parse(parametrosConfig["MenorPosicaoExibida"])
            };

            return parametros;
        }
    }
}

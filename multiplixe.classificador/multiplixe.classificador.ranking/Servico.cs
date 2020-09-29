using adduo.helper.extensionmethods;
using Microsoft.Extensions.Configuration;
using multiplixe.central_rtdb.client;
using multiplixe.empresas.client;
using System;
using System.Linq;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.ranking
{
    public class Servico
    {
        private Repositorio repositorio { get; }
        private EmpresaClient empresaClient { get; }
        private RTDBAtividadeComumClient rtdbAtividadeComumClient { get; }
        private IConfiguration configuration { get; }

        public Servico(
            Repositorio repositorio,
            EmpresaClient empresaClient,
            RTDBAtividadeComumClient rtdbAtividadeComumClient,
            IConfiguration configuration)
        {
            this.repositorio = repositorio;
            this.empresaClient = empresaClient;
            this.rtdbAtividadeComumClient = rtdbAtividadeComumClient;
            this.configuration = configuration;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.ranking.Ranking> Obter(Guid usuarioId)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.ranking.Ranking>();

            try
            {
                var menorPosicaoExibida = 10;

                if(!int.TryParse(configuration["Ranking:MenorPosicaoExibida"], out menorPosicaoExibida))
                {
                    Console.WriteLine("Configurar [Ranking:MenorPosicaoExibida] no appsettings.json");
                }

                var ranking = repositorio.Obter(usuarioId, menorPosicaoExibida);

                IdentificaUsuarioAtual(usuarioId, ranking);

                response.Item = ranking;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                // ## TODO log
            }

            return response;
        }

        private void IdentificaUsuarioAtual(Guid usuarioId, dto.ranking.Ranking ranking)
        {
            var posicaoUsuarioAtual = ranking.Posicoes.Where(s => s.UsuarioId == usuarioId).FirstOrDefault();

            if (posicaoUsuarioAtual.NotIsNull())
            {
                posicaoUsuarioAtual.UsuarioAtual = true;
            }
        }

        public void Processar()
        {
            var response = empresaClient.ObterAtivas();

            if (response.Success)
            {
                foreach (var empresa in response.Item)
                {
                    Console.WriteLine("--------------------------------------");

                    try
                    {
                        Console.WriteLine("- {0}", empresa.Nome);
                        repositorio.Calcular(empresa.Id);

                        rtdbAtividadeComumClient.RegistrarRanking();

                        Console.WriteLine("processado!", empresa.Nome);
                    }
                    catch (Exception ex)
                    {
                        // ## TODO log
                        Console.WriteLine("ERRO {0}", ex.Message);
                    }
                }
            }
            else
            {
                // ## TODO log

                throw new Exception("Erro ao buscar as empresas ativas");
            }
        }
    }
}

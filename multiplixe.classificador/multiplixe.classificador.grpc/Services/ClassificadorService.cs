using multiplixe.classificador.grpc.Protos;
using Grpc;
using System;
using System.Net;
using System.Threading.Tasks;
using classificacao = multiplixe.classificador.classificacao;
using Grpc.Core;

namespace multiplixe.classificador.grpc.Services
{
    public class ClassificadorService : Classificador.ClassificadorBase
    {
        private parsers.ObterClassificacao obterClassificacao { get; }
        private parsers.ObterPontuacaoTotal obterPontuacaoTotal { get; }
        private classificacao.Servico classificacaoService { get; }

        public ClassificadorService(
            parsers.ObterClassificacao obterClassificacao,
            parsers.ObterPontuacaoTotal obterPontuacaoTotal,
            classificacao.Servico classificacaoService)
        {
            this.obterClassificacao = obterClassificacao;
            this.obterPontuacaoTotal = obterPontuacaoTotal;
            this.classificacaoService = classificacaoService;
        }

        public override Task<ClassificacaoResponse> ObterClassificacao(ClassificacaoRequest classificacaoParametro, ServerCallContext context)
        {
            var response = new ClassificacaoResponse();

            try
            {
                var usuarioId = Guid.Parse(classificacaoParametro.UsuarioId);

                var classificacao = classificacaoService.Obter(usuarioId);

                response = obterClassificacao.Response(classificacao);
            }
            catch (Exception ex)
            {
                //## TODO log
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }

        public override Task<PontuacaoResponse> ObterPontuacaoTotal(ClassificacaoRequest classificacaoParametro, ServerCallContext context)
        {
            var response = new PontuacaoResponse();

            try
            {
                var usuarioId = Guid.Parse(classificacaoParametro.UsuarioId);

                var pontuacao = classificacaoService.ObterPontuacaoTotal(usuarioId);

                response = obterPontuacaoTotal.Response(pontuacao);
            }
            catch (Exception ex)
            {
                //## TODO log
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }

    }
}

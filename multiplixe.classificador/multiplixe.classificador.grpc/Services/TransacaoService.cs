using adduo.helper.extensionmethods;
using Grpc.Core;
using multiplixe.classificador.grpc.Protos;
using System;
using System.Net;
using System.Threading.Tasks;

namespace multiplixe.classificador.grpc.Services
{
    public class TransacaoService : Transacao.TransacaoBase
    {
        private parsers.TransacaoDebitar debitoParser { get; }
        private parsers.TransacaoEstornar estornoParser { get; }
        private transacao.Debito debitoServico { get; }
        private transacao.Estorno estornoServico { get; }

        public TransacaoService(
            parsers.TransacaoDebitar debitoParser,
            parsers.TransacaoEstornar estornoParser,
            transacao.Debito debitoServico,
            transacao.Estorno estornoServico)
        {
            this.debitoParser = debitoParser;
            this.estornoParser = estornoParser;
            this.debitoServico = debitoServico;
            this.estornoServico = estornoServico;
        }

        public override Task<DebitoResponse> Debitar(DebitoRequest request, ServerCallContext context)
        {
            var response = new DebitoResponse();

            try
            {
                var envelope = debitoServico.Processar(
                        request.UsuarioId.ToGuid(),
                        request.ParceiroId.ToGuid(),
                        request.Descricao,
                        request.ParceiroTransacaoId,
                        request.Pontos);

                response = debitoParser.Response(envelope);
            }
            catch (Exception ex)
            {
                //## TODO log

                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }

        public override Task<EstornoResponse> Estornar(EstornoRequest request, ServerCallContext context)
        {
            var response = new EstornoResponse();

            try
            {
                var envelope = estornoServico.Processar(
                        request.TransacaoId.ToGuid(),
                        request.ParceiroId.ToGuid());

                response = estornoParser.Response(envelope);
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

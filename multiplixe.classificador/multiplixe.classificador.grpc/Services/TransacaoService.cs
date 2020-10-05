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
        private parsers.TransacaoDebitar debitarParser { get; }
        private transacao.Debito servico { get; }

        public TransacaoService(
            parsers.TransacaoDebitar debitarParser,
            transacao.Debito servico)
        {
            this.debitarParser = debitarParser;
            this.servico = servico;
        }

        public override Task<DebitarResponse> Debitar(DebitarRequest request, ServerCallContext context)
        {
            var response = new DebitarResponse();

            try
            {
                var envelope = servico.Processar(
                        request.UsuarioId.ToGuid(),
                        request.ParceiroId,
                        request.Descricao,
                        request.ParceiroTransacaoId,
                        request.Pontos);

                response = debitarParser.Response(envelope);
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

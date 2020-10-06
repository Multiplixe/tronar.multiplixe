using adduo.helper.envelopes;
using multiplixe.classificador.interfaces;
using multiplixe.comum.dto.externo;
using System;

namespace multiplixe.classificador.transacao
{
    public class Estorno : BaseTransacao
    {
        public Estorno(
            Repositorio repositorio,
            Saldo saldoService,
            IConsultarUsuario consultarUsuario,
            IConsultarParceiro consultarParceiro) : base(repositorio, saldoService, consultarUsuario, consultarParceiro)
        {

        }

        public ResponseEnvelope<EstornoResponse> Processar(Guid transacaoId, Guid parceiroId)
        {
            var response = new ResponseEnvelope<EstornoResponse>();

            try
            {
                VerificarParceiroExistente(parceiroId);

                ValidarTransacao(transacaoId);

                var transacao = Obter(transacaoId, parceiroId);

                if (transacao != null)
                {
                    var id = Guid.NewGuid();
                    repositorio.Estornar(id, transacaoId, parceiroId);
                    saldoService.Processar(transacao.UsuarioId);

                    response.Item.Id = id;
                }
                else
                {
                    response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Error.Messages.Add($"Transaction {transacaoId} doesn't exist.");
                }
            }
            catch (ArgumentException aex)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Error.Messages.Add(aex.Message);
            }
            catch (Exception ex)
            {
                // ## TODO log
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }






    }
}

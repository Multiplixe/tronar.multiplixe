using adduo.helper.envelopes;
using multiplixe.classificador.interfaces;
using multiplixe.comum.dto.externo;
using multiplixe.enfileirador.client;
using System;

namespace multiplixe.classificador.transacao
{
    public class Debito : BaseTransacao
    {
        public Debito(
            Repositorio repositorio,
            Saldo saldoService,
            IConsultarUsuario consultarUsuario,
            IConsultarParceiro consultarParceiro,
            EnfileiradorClient enfileirador) : base(repositorio, saldoService, consultarUsuario, consultarParceiro, enfileirador)
        {

        }

        public ResponseEnvelope<DebitoResponse> Processar(Guid usuarioId, Guid parceiroId, string descricao, string parceiroTransacaoId, int pontos)
        {
            var response = new ResponseEnvelope<DebitoResponse>();

            try
            {
                VerificarUsuarioExistente(usuarioId);

                VerificarParceiroExistente(parceiroId);

                ValidarPontos(pontos);

                ValidarParceiroTransacao(parceiroTransacaoId);

                var id = Guid.NewGuid();

                var debitou = repositorio.Debitar(id, usuarioId, descricao, parceiroId, parceiroTransacaoId, pontos);

                if (debitou)
                {
                    response.Item.Id = id;
                    saldoService.Processar(usuarioId);

                    var UsuarioParaProcessar = new comum.dto.UsuarioParaProcessar(usuarioId);

                    enfileirador.EnfileirarParaPosClassificador(UsuarioParaProcessar);
                }
                else
                {
                    response.HttpStatusCode = System.Net.HttpStatusCode.PaymentRequired;
                    response.Error.Messages.Add("insufficient balance");
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

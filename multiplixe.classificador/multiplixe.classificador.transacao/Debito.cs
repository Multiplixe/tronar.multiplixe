using adduo.helper.envelopes;
using multiplixe.classificador.interfaces;
using multiplixe.comum.dto.externo;
using System;

namespace multiplixe.classificador.transacao
{
    public class Debito
    {
        private Repositorio repositorio { get; }
        private Saldo saldoService { get; }
        private IConsultarUsuario consultarUsuario { get; }
        private IConsultarParceiro consultarParceiro { get; }

        public Debito(
            Repositorio repositorio,
            Saldo saldoService,
            IConsultarUsuario consultarUsuario,
            IConsultarParceiro consultarParceiro)
        {
            this.repositorio = repositorio;
            this.saldoService = saldoService;
            this.consultarUsuario = consultarUsuario;
            this.consultarParceiro = consultarParceiro;
        }

        public ResponseEnvelope<DebitoResponse> Processar(Guid usuarioId, string parceiroId, string descricao, string parceiroTransacaoId, int pontos)
        {
            var response = new ResponseEnvelope<DebitoResponse>();

            try
            {
                VerificarUsuarioExistente(usuarioId);

                VerificarParceiroExistente(parceiroId);

                ValidarPontos(pontos);

                ValidarParceiroTransacao(parceiroTransacaoId);

                var id = Guid.NewGuid();

                var debitou = repositorio.Debitar(id, usuarioId, descricao, Guid.Parse(parceiroId), parceiroTransacaoId, pontos);

                if (debitou)
                {
                    response.Item.Id = id;
                    saldoService.Processar(usuarioId);
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

        private void VerificarUsuarioExistente(Guid usuarioId)
        {
            var existe = consultarUsuario.VerificarExistencia(usuarioId);

            if (!existe)
            {
                throw new ArgumentException($"[1001] Ivalid user (token)");
            }
        }

        private void VerificarParceiroExistente(string parceiroId)
        {
            var existe = false;

            var guid = Guid.Empty;

            if (Guid.TryParse(parceiroId, out guid))
            {
                existe = consultarParceiro.VerificarExistencia(guid);
            }

            if (!existe)
            {
                throw new ArgumentException($"[1002] Invalid partnerId: {parceiroId}");
            }
        }

        private void ValidarPontos(int pontos)
        {
            if (pontos <= 0)
            {
                throw new ArgumentException($"[1003] Invalid points: {pontos}");
            }
        }

        private void ValidarParceiroTransacao(string parceiroTransacaoId)
        {
            if (string.IsNullOrEmpty(parceiroTransacaoId))
            {
                throw new ArgumentException($"[1004] partnerTransactionId is empty");
            }
        }



    }
}

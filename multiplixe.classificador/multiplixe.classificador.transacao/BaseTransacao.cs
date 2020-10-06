using multiplixe.classificador.interfaces;
using multiplixe.enfileirador.client;
using System;

namespace multiplixe.classificador.transacao
{
    public class BaseTransacao
    {
        protected Repositorio repositorio { get; }
        protected Saldo saldoService { get; }
        protected IConsultarUsuario consultarUsuario { get; }
        protected IConsultarParceiro consultarParceiro { get; }
        protected EnfileiradorClient enfileirador { get; }

        public BaseTransacao(
            Repositorio repositorio,
            Saldo saldoService,
            IConsultarUsuario consultarUsuario,
            IConsultarParceiro consultarParceiro,
            EnfileiradorClient enfileirador)
        {
            this.repositorio = repositorio;
            this.saldoService = saldoService;
            this.consultarUsuario = consultarUsuario;
            this.consultarParceiro = consultarParceiro;
            this.enfileirador = enfileirador;
        }

        public results.Transacao Obter(Guid id, Guid parceiroId)
        {
            return repositorio.Obter(id, parceiroId);
        }

        protected void VerificarUsuarioExistente(Guid usuarioId)
        {
            var existe = consultarUsuario.VerificarExistencia(usuarioId);

            if (!existe)
            {
                throw new ArgumentException($"[1001] Ivalid user (token)");
            }
        }

        protected void VerificarParceiroExistente(Guid parceiroId)
        {
            var existe = consultarParceiro.VerificarExistencia(parceiroId);

            if (!existe)
            {
                throw new ArgumentException($"[1002] Invalid partnerId: {parceiroId}");
            }
        }

        protected void ValidarPontos(int pontos)
        {
            if (pontos <= 0)
            {
                throw new ArgumentException($"[1003] Invalid points: {pontos}");
            }
        }

        protected void ValidarParceiroTransacao(string parceiroTransacaoId)
        {
            if (string.IsNullOrEmpty(parceiroTransacaoId))
            {
                throw new ArgumentException($"[1004] partnerTransactionId is empty");
            }
        }

        protected void ValidarTransacao(Guid transacaoId)
        {
            if (transacaoId.Equals(Guid.Empty))
            {
                throw new ArgumentException($"[1005] transactionId is empty or invalid");
            }
        }


    }
}

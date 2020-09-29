using multiplixe.comum.triador;
using multiplixe.enfileirador.client;
using multiplixe.twitter.dto.eventos;
using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.twitter.reacao.triador.evento_triado
{
    public class ReacaoCurtida : EventoTriado
    {
        private coreinterfaces.triador.IRegistradorEventoTriagem<EventoReacao> registradorReacao { get; }
        private coreinterfaces.triador.IAvaliadorDeEvento<EventoReacao> avaliadorDeEvento { get; }
        private EnfileiradorClient enfileirador { get; }

        private comum_dto.EnvelopeEvento<EventoReacao> envelope { get; set; }

        public ReacaoCurtida(
             coreinterfaces.triador.IAvaliadorDeEvento<EventoReacao> avaliadorDeEvento,
             coreinterfaces.triador.IRegistradorEventoTriagem<EventoReacao> registradorReacao,
            EnfileiradorClient enfileirador,
            comum_dto.EnvelopeEvento<EventoReacao> envelope)
        {
            this.avaliadorDeEvento = avaliadorDeEvento;
            this.registradorReacao = registradorReacao;
            this.enfileirador = enfileirador;
            this.envelope = envelope;
        }

        public override void EnfileirarEvento()
        {
            enfileirador.EnfileirarParaPontuadorReacaoTwitter(envelope);
        }

        public override void RegistrarEvento()
        {
            registradorReacao.RegistrarEvento(envelope);
        }

        public override bool Avaliar()
        {
            return avaliadorDeEvento.Avaliar(envelope);
        }
    }
}

using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;
using multiplixe.facebook.dto.eventos;
using multiplixe.comum.triador;
using multiplixe.enfileirador.client;

namespace multiplixe.facebook.reacao.triador
{
    public class Reacao : EventoTriado
    {
        protected coreinterfaces.triador.IRegistradorEventoTriagem<Evento> registradorReacao { get; }
        protected coreinterfaces.triador.IAvaliadorDeEvento<Evento> avaliadorDeEvento { get; }
        protected EnfileiradorClient enfileirador { get; }
        protected comum_dto.EnvelopeEvento<Evento> envelope { get; set; }

        public Reacao(
                coreinterfaces.triador.IRegistradorEventoTriagem<Evento> registradorReacao,
                coreinterfaces.triador.IAvaliadorDeEvento<Evento> avaliadorDeEvento,
                EnfileiradorClient enfileirador,
                 comum_dto.EnvelopeEvento<Evento> envelope)
        {
            this.registradorReacao = registradorReacao;
            this.avaliadorDeEvento = avaliadorDeEvento;
            this.enfileirador = enfileirador;
            this.envelope = envelope;
        }

        public override bool Avaliar()
        {
            return this.avaliadorDeEvento.Avaliar(envelope);
        }

        public override void RegistrarEvento()
        {
            this.registradorReacao.RegistrarEvento(envelope);
        }

        public override void EnfileirarEvento()
        {
            this.enfileirador.EnfileirarParaPontuadorReacaoFacebook(envelope);
        }
    }
}

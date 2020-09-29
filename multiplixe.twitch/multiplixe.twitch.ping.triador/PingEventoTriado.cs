using multiplixe.comum.triador;
using multiplixe.enfileirador.client;
using multiplixe.twitch.dto.eventos;
using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.twitch.ping.triador
{
    public class PingEventoTriado : EventoTriado
    {
        private coreinterfaces.triador.IRegistradorEventoTriagem<EventoPing> registradorPing { get; }
        private coreinterfaces.triador.IAvaliadorDeEvento<EventoPing> avaliadorDeEvento { get; }
        private EnfileiradorClient enfileirador { get; }
        private comum_dto.EnvelopeEvento<EventoPing> envelope { get; }

        public PingEventoTriado(
            coreinterfaces.triador.IRegistradorEventoTriagem<EventoPing> registradorPing,
            coreinterfaces.triador.IAvaliadorDeEvento<EventoPing> avaliadorDeEvento,
            EnfileiradorClient enfileirador,
            comum_dto.EnvelopeEvento<Evento> _envelope)
        {
            this.registradorPing = registradorPing;
            this.enfileirador = enfileirador;
            this.avaliadorDeEvento = avaliadorDeEvento;
            this.envelope = _envelope.Transformar<EventoPing>(_envelope.Evento.Ping);
        }

        public override void EnfileirarEvento()
        {
            enfileirador.EnfileirarParaPontuadorPingTwitch(envelope);
        }

        public override void RegistrarEvento()
        {
            registradorPing.RegistrarEvento(envelope);
        }

        public override bool Avaliar()
        {
            return avaliadorDeEvento.Avaliar(envelope);
        }
    }
}

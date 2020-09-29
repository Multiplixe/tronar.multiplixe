using multiplixe.comum.dto;
using multiplixe.enfileirador.client;
using multiplixe.youtube.dto.eventos;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.youtube.livehashtag.triador
{
    public class EventoTriado : coreinterfaces.triador.IEventoTriado
    {
        private EnvelopeEvento<LiveHashtag> envelope { get; }

        public EventoTriado(EnvelopeEvento<dto.eventos.LiveHashtag> envelope)
        {
            this.envelope = envelope;
        }

        public bool Avaliar()
        {
            var validador = new Avaliador();

            return validador.Avaliar(envelope);
        }

        public void EnfileirarEvento()
        {
            var enfileirador = new EnfileiradorClient();

            enfileirador.EnfileirarParaPontuadorLiveHashtagYoutube(envelope);
        }

        public void RegistrarEvento()
        {
            var registrador = new Registrador();
            registrador.RegistrarEvento(envelope);
        }
    }
}

using multiplixe.comum.interfaces.triador;
using multiplixe.comum.triador;
using multiplixe.enfileirador.client;
using multiplixe.twitch.dto.eventos;
using multiplixe.twitch.ping.triador;
using comum_dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;

namespace multiplixe.twitch.triador.console
{
    public class Triador : ITriador<Evento>
    {
        private IRegistradorEventoTriagem<EventoPing> registradorEventoPing { get; }
        private IAvaliadorDeEvento<EventoPing> avaliadorDeEvento { get; }
        private EnfileiradorClient enfileiradorClient { get; }

        public Triador(
            IRegistradorEventoTriagem<EventoPing> registradorEventoPing,
            IAvaliadorDeEvento<EventoPing> avaliadorDeEvento,
            EnfileiradorClient client)
        {
            this.registradorEventoPing = registradorEventoPing;
            this.avaliadorDeEvento = avaliadorDeEvento;
            enfileiradorClient = client;
        }

        public IEventoTriado Triar(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            IEventoTriado eventoTriado = new EventoTriadoNullable();

            if (envelope.Evento.TipoEvento == enums.TipoEventoEnum.ping)
            {
                eventoTriado = new PingEventoTriado(
                    registradorEventoPing,
                    avaliadorDeEvento,
                    enfileiradorClient,
                    envelope); ;
            }

            return eventoTriado;
        }


    }
}

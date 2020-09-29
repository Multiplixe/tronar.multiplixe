using multiplixe.comum.enums;
using multiplixe.twitch.dto.eventos;
using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;
using registrador = multiplixe.registrador_de_eventos.client.twitch;

namespace multiplixe.twitch.ping.triador
{
    public class Registrador : coreinterfaces.triador.IRegistradorEventoTriagem<EventoPing>
    {
        private registrador.Client twitchClient { get; }

        public Registrador(registrador.Client twitchClient)
        {
            this.twitchClient = twitchClient;
        }

        public void RegistrarEvento(comum_dto.EnvelopeEvento<EventoPing> envelope)
        {
            this.twitchClient.RegistrarPing(
                envelope.Id, 
                envelope.UsuarioId, 
                envelope.Evento.PerfilId, 
                envelope.Evento.PostId, 
                envelope.DataEvento, 
                envelope.Evento.Ultimo, 
                envelope.Evento.Atual, 
                envelope.Evento.ToleranciaSegundos,
                envelope.Evento.FrequenciaMinutos,
                envelope.Evento.PausaMilissegundos, 
                TipoEventoEnum.ping);
        }
    }
}

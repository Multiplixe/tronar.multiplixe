using multiplixe.comum.dto;
using multiplixe.comum.interfaces.triador;
using multiplixe.comum.triador;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.youtube.triador.console
{
    public class Triador : coreinterfaces.triador.ITriador<dto.eventos.Evento>
    {
        public Triador()
        {

        }

        public IEventoTriado Triar(EnvelopeEvento<dto.eventos.Evento> envelope)
        {
            IEventoTriado eventoTriado = new EventoTriadoNullable();

            if(envelope.Evento.Tipo == comum.enums.TipoEventoEnum.hashtag)
            {
                var e = envelope.Transformar<dto.eventos.LiveHashtag>(envelope.Evento.ObterLiveHashtag());
                eventoTriado = new livehashtag.triador.EventoTriado(e);
            }

            return eventoTriado;
        }
    }
}

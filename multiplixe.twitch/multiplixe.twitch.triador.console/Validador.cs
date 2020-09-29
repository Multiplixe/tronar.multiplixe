using multiplixe.comum.exceptions;
using multiplixe.comum.interfaces.triador;
using multiplixe.twitch.dto.eventos;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.twitch.triador.console
{
    public class Validador : IValidadorDeEvento<Evento>
    {
        public void Validar(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            if (envelope.Evento == null ||
                envelope.Evento.Ping == null)
            {
                throw new EventoInvalidoException(envelope.Evento);
            }
        }
    }
}

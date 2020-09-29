using multiplixe.facebook.dto.eventos;
using comum_dto = multiplixe.comum.dto;
using coreexceptions = multiplixe.comum.exceptions;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.facebook.triador.console
{
    public class Validador : coreinterfaces.triador.IValidadorDeEvento<Evento>
    {
        public void Validar(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            var eventoFacade = new EventoFacade(envelope.Evento);

            if (!eventoFacade.Validar())
            {
                throw new coreexceptions.EventoInvalidoException(envelope.Evento);
            }
        }
    }
}

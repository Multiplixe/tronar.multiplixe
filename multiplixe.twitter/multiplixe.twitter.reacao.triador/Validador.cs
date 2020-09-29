using multiplixe.comum.exceptions;
using coreinterfaces = multiplixe.comum.interfaces;
using comum_dto = multiplixe.comum.dto;
using multiplixe.twitter.dto.eventos;

namespace multiplixe.twitter.reacao.triador
{
    public class Validador : coreinterfaces.triador.IValidadorDeEvento<EventoReacao>
    {
        public void Validar(comum_dto.EnvelopeEvento<EventoReacao> envelope)
        {
            var eventoFacade = new EventoReacaoFacade(envelope.Evento);

            if (!eventoFacade.Validar())
            {
                throw new EventoInvalidoException(envelope.Evento);
            }
        }
    }
}

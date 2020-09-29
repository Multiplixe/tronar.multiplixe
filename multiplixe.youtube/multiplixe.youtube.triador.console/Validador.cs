using multiplixe.comum.dto;
using multiplixe.comum.exceptions;
using multiplixe.youtube.dto.eventos;
using System;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.youtube.triador.console
{
    public class Validador : coreinterfaces.triador.IValidadorDeEvento<dto.eventos.Evento>
    {
        public void Validar(EnvelopeEvento<Evento> envelope)
        {
            if (envelope.EmpresaId == Guid.Empty || 
                envelope.Evento == null)
            {
                throw new EventoInvalidoException(envelope.Evento);
            }
        }
    }
}

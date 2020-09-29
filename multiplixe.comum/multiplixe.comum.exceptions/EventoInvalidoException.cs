using System;

namespace multiplixe.comum.exceptions
{
    public class EventoInvalidoException : Exception
    {
        public object Evento { get; set; }
        public EventoInvalidoException()
        {

        }

        public EventoInvalidoException(object evento)
        {
            Evento = evento;
        }
    }
}

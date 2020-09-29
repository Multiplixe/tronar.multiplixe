using multiplixe.comum.dto;

namespace multiplixe.comum.interfaces.triador
{
    public interface ITriador<T> where T : dto.EventoBase
    {
        IEventoTriado Triar(EnvelopeEvento<T> evento);
    }
}

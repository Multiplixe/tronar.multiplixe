using multiplixe.comum.dto;

namespace multiplixe.comum.interfaces.receptor
{
    public interface IReceptorService<T> where T : dto.EventoBase
    {
        void ProcessarEvento(EnvelopeEvento<T> evento);
    }
}
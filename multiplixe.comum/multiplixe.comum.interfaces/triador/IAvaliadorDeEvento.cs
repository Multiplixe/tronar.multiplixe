using multiplixe.comum.dto;

namespace multiplixe.comum.interfaces.triador
{
    public interface IAvaliadorDeEvento<T> where T : dto.EventoBase
    {
        bool Avaliar(EnvelopeEvento<T> envelope);
    }
}

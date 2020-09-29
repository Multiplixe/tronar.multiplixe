using multiplixe.comum.dto;

namespace multiplixe.comum.interfaces.triador
{
    public interface IValidadorDeEvento<T> where T : dto.EventoBase
    {
        void Validar(EnvelopeEvento<T> envelope);
    }
}

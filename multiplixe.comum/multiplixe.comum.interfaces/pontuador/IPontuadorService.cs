using multiplixe.comum.dto;

namespace multiplixe.comum.interfaces.pontuador
{
    public interface IPontuadorService<T> where T : dto.EventoBase
    {
        void ProcessarEvento(EnvelopeEvento<T> envelope);
    }
}

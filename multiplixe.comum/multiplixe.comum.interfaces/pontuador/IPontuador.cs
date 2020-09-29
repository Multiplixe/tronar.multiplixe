namespace multiplixe.comum.interfaces.pontuador
{
    public interface IPontuador<T> where T : dto.EventoBase
    {
        dto.Ponto Pontuar(dto.EnvelopeEvento<T> evento);
    }
}

namespace multiplixe.comum.interfaces
{
    public interface IRegistradorEventosConsultas<T> where T : dto.EventoBase
    {
        dto.Reacao ObterUltimaReacao(dto.EnvelopeEvento<T> envelope);
    }
}

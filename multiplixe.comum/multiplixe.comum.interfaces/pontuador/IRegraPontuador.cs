namespace multiplixe.comum.interfaces.pontuador
{
    public interface IRegraPontuador<T> where T : dto.EventoBase
    {
        int Pontuar(T evento);
    }
}
namespace multiplixe.comum.interfaces.receptor
{
    public interface IEventoValidador<T> where T : dto.EventoBase
    {
        void Validar(T evento);

    }
}

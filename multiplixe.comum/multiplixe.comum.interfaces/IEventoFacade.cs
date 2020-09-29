namespace multiplixe.comum.interfaces
{
    public interface IEventoFacade
    {
        string PerfilId { get; }
        string PerfilNome { get; }
        string PostId { get; }

        bool Validar();
    }
}

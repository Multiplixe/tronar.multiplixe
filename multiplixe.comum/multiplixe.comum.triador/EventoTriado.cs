using multiplixe.comum.interfaces.triador;

namespace multiplixe.comum.triador
{
    public abstract class EventoTriado : IEventoTriado
    {

        public abstract void EnfileirarEvento();

        public abstract void RegistrarEvento();

        public abstract bool Avaliar();

    }
}

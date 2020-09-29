using multiplixe.comum.interfaces.triador;

namespace multiplixe.comum.triador
{
    public class EventoTriadoNullable : IEventoTriado
    {
        public void EnfileirarEvento()
        {
        }

        public void RegistrarEvento()
        {
        }

        public bool Avaliar()
        {
            return false;
        }
    }
}

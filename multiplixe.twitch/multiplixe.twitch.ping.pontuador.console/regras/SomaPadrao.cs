using multiplixe.comum.interfaces.pontuador;
using multiplixe.twitch.dto.eventos;

namespace multiplixe.twitch.ping.pontuador.console.regras
{
    internal class SomaPadrao : IRegraPontuador<EventoPing>
    {
        public int Pontuar(EventoPing evento)
        {
            return 1;
        }
    }
}
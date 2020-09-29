using multiplixe.comum.interfaces.pontuador;
using multiplixe.twitch.dto.eventos;

namespace multiplixe.twitch.ping.pontuador.console.regras
{
    public class Factory
    {
        public static IRegraPontuador<EventoPing> RegraPontuacaoPadrao(EventoPing evento)
        {
            IRegraPontuador<EventoPing> regra = new SomaPadrao();

            return regra;

        }
    }
}

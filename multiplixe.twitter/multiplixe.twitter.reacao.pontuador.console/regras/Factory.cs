using multiplixe.twitter.dto.eventos;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.twitter.reacao.pontuador.console.regras
{
    public class Factory
    {
        public static coreinterfaces.pontuador.IRegraPontuador<EventoReacao> ObtemRegra(EventoReacao evento)
        {
            coreinterfaces.pontuador.IRegraPontuador<EventoReacao> regra = new SomaPadrao();

            return regra;
        }
    }
}

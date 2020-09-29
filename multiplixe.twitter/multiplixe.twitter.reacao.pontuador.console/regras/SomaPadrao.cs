using multiplixe.twitter.dto.eventos;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.twitter.reacao.pontuador.console.regras
{
    public class SomaPadrao : coreinterfaces.pontuador.IRegraPontuador<EventoReacao>
    {
        public int Pontuar(EventoReacao evento)
        {
            return 1;
        }
    }
}

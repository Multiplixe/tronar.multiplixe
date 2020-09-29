using multiplixe.facebook.dto.eventos;
using coreinterfaces = game.core.interfaces;

namespace multiplixe.facebook.reacao.pontuador.regra
{
    public class SomaPadrao : coreinterfaces.pontuador.IRegraPontuador<Evento>
    {
        public int Pontuar(Evento evento)
        {
            return 1;
        }
    }
}
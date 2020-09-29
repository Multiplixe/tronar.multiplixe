using multiplixe.facebook.dto.eventos;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.facebook.reacao.pontuador.console.regra
{
    public class SubtracaoPadrao : coreinterfaces.pontuador.IRegraPontuador<Evento>
    {
        public int Pontuar(Evento evento)
        {
            return -1;
        }
    }
}
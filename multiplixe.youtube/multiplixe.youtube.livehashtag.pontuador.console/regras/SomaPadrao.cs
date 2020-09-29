using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.youtube.livehashtag.pontuador.console.regras
{
    public class RegraSomaPadrao : coreinterfaces.pontuador.IRegraPontuador<dto.eventos.LiveHashtag>
    {
        public int Pontuar(dto.eventos.LiveHashtag evento)
        {
            return 1;
        }
    }
}

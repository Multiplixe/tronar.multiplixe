using System;
using System.Collections.Generic;
using System.Text;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.youtube.livehashtag.pontuador.console.regras
{
    public class Factory
    {
        public static coreinterfaces.pontuador.IRegraPontuador<dto.eventos.LiveHashtag> ObtemRegra(dto.eventos.LiveHashtag evento)
        {
            coreinterfaces.pontuador.IRegraPontuador<dto.eventos.LiveHashtag> regra = new RegraSomaPadrao();

            return regra;
        }
    }
}

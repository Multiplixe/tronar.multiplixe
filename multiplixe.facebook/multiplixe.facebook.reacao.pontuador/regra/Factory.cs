using multiplixe.facebook.dto.enums;
using multiplixe.facebook.dto.eventos;
using coreinterfaces = game.core.interfaces;
using coreservices = game.core.services;

namespace multiplixe.facebook.reacao.pontuador.regra
{
    public class Factory
    {
        public static coreinterfaces.pontuador.IRegraPontuador<Evento> ObtemRegra(Evento evento)
        {

            coreinterfaces.pontuador.IRegraPontuador<Evento> regra = new coreservices.pontuador.RegraNulllable<Evento>();

            var dto = new EventoFacade(evento);

            var value = dto.Value;

            regra = RegraPontuacaoPadrao(value);

            return regra;
        }

        private static coreinterfaces.pontuador.IRegraPontuador<Evento> RegraPontuacaoPadrao(Value value)
        {
            coreinterfaces.pontuador.IRegraPontuador<Evento> regra = new SomaPadrao();

            if (value.verb == EventoVerbEnum.remove.ToString())
            {
                regra = new SubtracaoPadrao();
            }

            return regra;

        }

    }
}

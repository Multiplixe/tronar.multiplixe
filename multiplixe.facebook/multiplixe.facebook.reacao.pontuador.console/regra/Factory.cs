using multiplixe.comum.pontuador;
using multiplixe.facebook.dto.enums;
using multiplixe.facebook.dto.eventos;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.facebook.reacao.pontuador.console.regra
{
    public class Factory
    {
        public static coreinterfaces.pontuador.IRegraPontuador<Evento> ObtemRegra(Evento evento)
        {

            coreinterfaces.pontuador.IRegraPontuador<Evento> regra = new RegraNulllable<Evento>();

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

using coreenums = multiplixe.comum.enums;
using coreinterfaces = multiplixe.comum.interfaces;
using System.Linq;

namespace multiplixe.facebook.dto.eventos
{
    public class EventoFacade : coreinterfaces.IEventoFacade
    {
        private Evento dto { get; }

        public Value Value { get { return ObterValue(); } }
        public string PerfilId { get { return ObterPerfilId(); } }
        public string PerfilNome { get { return ObterPerfilNome(); } }
        public string PostId { get { return ObterParentId(); } }
        public string Intensidade { get { return ObterIntensidade(); } }

        public coreenums.TipoEventoEnum Tipo { get { return ObterReacaoTipo(); } }

        public EventoFacade(Evento dto)
        {
            this.dto = dto;
        }

        private string ObterPerfilNome()
        {
            var value = ObterValue();
            return value.from.name;
        }

        private string ObterPerfilId()
        {
            var value = ObterValue();
            return value.from.id;
        }

        private string ObterParentId()
        {
            var value = ObterValue();
            return value.parent_id;
        }


        private Value ObterValue()
        {
            return dto.entry[0].changes[0].value;
        }

        private string ObterIntensidade()
        {
            var value = ObterValue();
            return value.reaction_type;
        }

        private coreenums.TipoEventoEnum ObterReacaoTipo()
        {
            var value = ObterValue();
            return value.verb == enums.EventoVerbEnum.add.ToString() ? coreenums.TipoEventoEnum.curtida : coreenums.TipoEventoEnum.descurtida;
        }

        public bool Validar()
        {
            return dto != null &&
                    dto.entry != null &&
                    dto.entry.Any() &&
                    dto.entry[0].changes != null &&
                    dto.entry[0].changes.Any() &&
                    dto.entry[0].changes[0].value != null &&
                    dto.entry[0].changes[0].value.parent_id != null &&
                    dto.entry[0].changes[0].value.post_id != null &&
                    dto.entry[0].changes[0].value.reaction_type != null &&
                    dto.entry[0].changes[0].value.from != null &&
                    dto.entry[0].changes[0].value.from.id != null;
        }
    }
}

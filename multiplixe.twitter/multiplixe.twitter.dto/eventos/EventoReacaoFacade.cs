using multiplixe.comum.enums;

namespace multiplixe.twitter.dto.eventos
{
    public class EventoReacaoFacade
    {
        private EventoReacao evento { get; }

        public string PerfilId { get { return ObterPerfilId(); } }
        public string PerfilNome { get { return ObterPerfilNome(); } }
        public string PostId { get { return ObterPostId(); } }

        public TipoEventoEnum Tipo { get { return ObterReacaoTipo(); } }

        public EventoReacaoFacade(EventoReacao evento)
        {
            this.evento = evento;
        }

        public string ObterPerfilId()
        {
            return evento.user.id.ToString();
        }

        public string ObterPerfilNome()
        {
            return evento.user.name;
        }

        public string ObterPostId()
        {
            return evento.favorited_status.id.ToString();
        }

        public TipoEventoEnum ObterReacaoTipo()
        {
            return TipoEventoEnum.curtida;
        }

        public bool Validar()
        {
            return evento != null &&
                    evento.user != null &&
                    !evento.user.id.Equals(0) &&
                    !string.IsNullOrEmpty(evento.user.name) &&
                    evento.favorited_status != null &&
                    !evento.favorited_status.id.Equals(0);
        }
    }
}

using multiplixe.comum.dto;
using multiplixe.comum.enums;

namespace multiplixe.youtube.dto.eventos
{
    public class Evento : EventoBase
    {
        public TipoEventoEnum Tipo { get; set; }
        public string PerfilId { get; set; }
        public LiveHashtag LiveHashtag { get; set; }

        public Evento()
        {

        }

        public Evento(string perfilId, string LiveChatId, string hashtag)
        {
            Tipo = TipoEventoEnum.hashtag;
            PerfilId = perfilId;
            LiveHashtag = new LiveHashtag()
            {
                PerfilId = perfilId,
                PostId = LiveChatId,
                Hashtag = hashtag
            };
        }

        public LiveHashtag ObterLiveHashtag()
        {
            return LiveHashtag;
        }
    }
}

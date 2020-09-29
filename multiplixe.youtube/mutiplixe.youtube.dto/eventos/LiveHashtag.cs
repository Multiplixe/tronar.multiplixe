
using multiplixe.comum.dto;

namespace multiplixe.youtube.dto.eventos
{
    public class LiveHashtag : EventoBase
    {
        public string PerfilId { get; set; }
        public string PostId { get; set; }
        public string Hashtag { get; set; }
    }
}

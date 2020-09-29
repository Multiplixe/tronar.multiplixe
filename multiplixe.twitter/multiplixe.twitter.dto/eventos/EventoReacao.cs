using comum_dto = multiplixe.comum.dto;

namespace multiplixe.twitter.dto.eventos
{
    public class EventoReacao : comum_dto.EventoBase
    {
        public string id { get; set; }

        public string created_at { get; set; }

        public TweetInfo favorited_status { get; set; }

        public EventUser user { get; set; }
    }
}

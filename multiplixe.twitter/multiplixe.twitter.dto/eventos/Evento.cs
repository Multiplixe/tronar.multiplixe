using comum_dto = multiplixe.comum.dto;
using System.Collections.Generic;

namespace multiplixe.twitter.dto.eventos
{
    public class Evento : comum_dto.EventoBase
    {
        public List<ReTweetCreateEventInformation> tweet_create_events { get; set; }

        public List<ReTweetDeleteEventInformation> tweet_delete_events { get; set; }

        public List<EventoReacao> favorite_events { get; set; }

        public List<FollowEventInformation> follow_events { get; set; }

        public List<EventUser> users { get; set; }

        public string next_cursor_str { get; set; }

        public Evento()
        {
            tweet_create_events = new List<ReTweetCreateEventInformation>();
            tweet_delete_events = new List<ReTweetDeleteEventInformation>();
            favorite_events = new List<EventoReacao>();
            follow_events = new List<FollowEventInformation>();
            users = new List<EventUser>();
        }

        //[JsonProperty("unfollow_events")]
        //public List<EventInformation> Unfollow { get; set; }
    }
}

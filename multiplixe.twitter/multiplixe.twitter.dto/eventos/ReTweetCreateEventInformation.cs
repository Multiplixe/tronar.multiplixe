using System.Collections.Generic;

namespace multiplixe.twitter.dto.eventos
{
    public class ReTweetCreateEventInformation
    {
        public string id { get; set; }

        public string created_at { get; set; }

        public TweetInfo retweeted_status { get; set; }

        public EventUser user { get; set; }

        public List<int> display_text_range { get; set; }

        public string text { get; set; }

        public long? in_reply_to_status_id { get; set; }
    }
}

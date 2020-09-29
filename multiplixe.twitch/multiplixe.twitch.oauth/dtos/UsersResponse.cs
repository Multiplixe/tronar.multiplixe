using System.Collections.Generic;

namespace multiplixe.twitch.oauth.dtos
{
    public class UsersResponse
    {
        public List<User> data { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string login { get; set; }
        public string display_name { get; set; }
        public string type { get; set; }
        public string broadcaster_type { get; set; }
        public string profile_image_url { get; set; }
        public string offline_image_url { get; set; }
        public int view_count { get; set; }
    }
}

namespace multiplixe.facebook.autenticacao.dtos
{
    public class UserResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public UserPictureResponse picture { get; set; }
        public ErrorResponse error { get; set; }

    }

    public class UserPictureResponse
    {
        public UserPictureDataResponse data { get; set; }
    }

    public class UserPictureDataResponse
    {
        public int height { get; set; }
        public int width { get; set; }
        public bool is_silhouette { get; set; }
        public string url { get; set; }
    }

}

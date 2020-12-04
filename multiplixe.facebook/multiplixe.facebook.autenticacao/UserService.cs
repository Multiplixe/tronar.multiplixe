using multiplixe.comum.helper;
using multiplixe.facebook.autenticacao.dtos;

namespace multiplixe.facebook.autenticacao
{
    public class UserService
    {
        public UserResponse Obter(string token)
        {
            var url = $"https://graph.facebook.com/me?fields=id,name,short_name,picture&access_token={token}";

            var response = WebRequestHelper.GetExterno<UserResponse>(url);

            return response.Item;
        }
    }
}

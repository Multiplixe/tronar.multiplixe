using System.Text.Json.Serialization;

namespace multiplixe.usuarios.externo.autenticacao
{
    public class Parametros
    {
        public string firebase_api_key { get; set; }
        public string external_secret_key { get; set; }
    }
}

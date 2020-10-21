using multiplixe.comum.dto;
using multiplixe.comum.helper;
using multiplixe.empresas.client;
using System;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil.access_token.twitter
{
    public class ProcessadorToken : ServicoBase, IProcessarAccessToken
    {

        public ProcessadorToken(Guid empresaId, EmpresaClient empresaClient) : base(empresaId, empresaClient)
        {
        }

        /// <summary>
        /// Twitter não tem troca de token.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public PerfilAccessToken TrocarToken(string json)
        {
            var accessToken = DeserializadorHelper.Deserializar<twitter.AccessToken>(json);

            var perfilToken = new comum_dto.PerfilAccessToken
            {
                Token = accessToken.oauth_token,
                Secret = accessToken.oauth_token_secret
            };

            perfilToken.Json = SerializadorHelper.Serializar(perfilToken);

            return perfilToken;
        }
    }
}

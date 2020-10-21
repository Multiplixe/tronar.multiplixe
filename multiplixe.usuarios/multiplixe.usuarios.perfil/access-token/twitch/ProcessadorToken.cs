using multiplixe.comum.dto;
using multiplixe.comum.helper;
using multiplixe.empresas.client;
using System;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil.access_token.twitch
{
    public class ProcessadorToken : ServicoBase, IProcessarAccessToken
    {

        public ProcessadorToken(Guid empresaId, EmpresaClient empresaClient) : base(empresaId, empresaClient)
        {
        }

        /// <summary>
        /// Twitch não tem troca de token.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public PerfilAccessToken TrocarToken(string json)
        {
            var accessToken = DeserializadorHelper.Deserializar<twitch.AccessToken>(json);

            var perfilToken = new comum_dto.PerfilAccessToken
            {
                Token = accessToken.access_token,
                Refresh = accessToken.refresh_token,
                Tipo = accessToken.token_type
            };

            perfilToken.Json = SerializadorHelper.Serializar(perfilToken);

            return perfilToken;
        }
    }
}

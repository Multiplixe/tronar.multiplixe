using multiplixe.comum.dto;
using multiplixe.comum.helper;
using multiplixe.empresas.client;
using System;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil.access_token.youtube
{
    public class ProcessadorToken : ServicoBase, IProcessarAccessToken
    {

        public ProcessadorToken(Guid empresaId, EmpresaClient empresaClient) : base(empresaId, empresaClient)
        {
        }

        /// <summary>
        /// youtube não tem troca de token.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public PerfilAccessToken TrocarToken(string json)
        {
            var accessToken = DeserializadorHelper.Deserializar<youtube.AccessToken>(json);

            var expires_in = 0;

            int.TryParse(accessToken.expires_in, out expires_in);

            var perfilToken = new comum_dto.PerfilAccessToken
            {
                Token = accessToken.access_token,
                Expiracao = DateTimeHelper.Now().AddSeconds(expires_in),
                Tipo = accessToken.token_type
            };

            perfilToken.Json = SerializadorHelper.Serializar(perfilToken);

            return perfilToken;
        }
    }
}

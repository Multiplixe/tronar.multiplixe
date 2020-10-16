using adduohelper = adduo.helper;
using comum_dto = multiplixe.comum.dto;
using multiplixe.comum.enums;
using multiplixe.usuarios.perfil.grpc.Protos;
using System;
using System.Net;

namespace multiplixe.usuarios.client.parsers
{
    public class PerfilObterAccessToken
    {
        public AccessTokenRequest Request(Guid usuarioId, RedeSocialEnum redeSocial)
        {
            return new AccessTokenRequest
            {
                UsuarioId = usuarioId.ToString(),
                RedeSocial = (int)redeSocial
            };
        }

        public adduohelper.envelopes.ResponseEnvelope<comum_dto.Token> Response(AccessTokenResponse accessTokenResponse)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<comum_dto.Token>();

            response.HttpStatusCode = (HttpStatusCode)accessTokenResponse.HttpStatusCode;

            if(response.Success)
            {
                response.Item = new comum_dto.Token
                {
                    Tipo = TipoTokenEnum.AccessTokenRedeSocial,
                    Valor = accessTokenResponse.Token
                };
            }

            return response;
        }
    }
}

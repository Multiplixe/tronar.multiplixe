using adduo.helper.envelopes;
using multiplixe.comum.enums;
using System;
using System.Net;
using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.perfil.grpc.Protos;

namespace multiplixe.usuarios.client.parsers
{
    class PerfilObter
    {
        public ResponseEnvelope<dto.Perfil> Response(proto.PerfilResponse perfilResponse)
        {
            var envelopeResponse = new ResponseEnvelope<dto.Perfil>
            {
                HttpStatusCode = (HttpStatusCode)perfilResponse.HttpStatusCode,
            };

            if (envelopeResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                envelopeResponse.Item = new dto.Perfil
                {
                    Nome = perfilResponse.Perfil.Nome,
                    UsuarioId = Guid.Parse(perfilResponse.Perfil.UsuarioId),
                    EmpresaId = Guid.Parse(perfilResponse.Perfil.EmpresaId),
                    PerfilId = perfilResponse.Perfil.PerfilId,
                    RedeSocial = (RedeSocialEnum)perfilResponse.Perfil.RedeSocial,
                    DataCadastro = new DateTime(perfilResponse.Perfil.DataCadastro),
                    Ativo = perfilResponse.Perfil.Ativo,
                    Token = perfilResponse.Perfil.Token,
                    ImagemUrl = perfilResponse.Perfil.ImagemUrl,
                    Login = perfilResponse.Perfil.Login,
                };
            }

            return envelopeResponse;
        }
    }
}

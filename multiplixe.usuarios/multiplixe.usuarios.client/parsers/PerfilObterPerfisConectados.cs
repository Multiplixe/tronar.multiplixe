using adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using multiplixe.comum.enums;
using multiplixe.usuarios.perfil.grpc.Protos;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace multiplixe.usuarios.client.parsers
{
    class PerfilObterPerfisConectados
    {
        public PerfilFiltro Request(Guid usuarioId)
        {
            return new PerfilFiltro
            {
                UsuarioId = usuarioId.ToString(),
                EmpresaId = string.Empty,
                PerfilId = string.Empty,
                RedeSocial = 0
            };
        }

        public ResponseEnvelope<dto.RedesSociaisPerfisConectados> Response(PerfilConectadoResponse perfilConectadoResponse)
        {
            var response = new ResponseEnvelope<dto.RedesSociaisPerfisConectados>();

            response.HttpStatusCode = (HttpStatusCode)perfilConectadoResponse.HttpStatusCode;

            response.Item.TemConexao = perfilConectadoResponse.TemConexao;

            foreach (var perfil in perfilConectadoResponse.Perfis)
            {
                response.Item.Perfis.Add(new dto.Perfil
                {
                    PerfilId = perfil.PerfilId,
                    Nome = perfil.Nome,
                    Login = perfil.Login,
                    RedeSocial = (RedeSocialEnum)perfil.RedeSocial,
                    ImagemUrl = perfil.ImagemUrl
                });

            }

            return response;
        }
    }
}

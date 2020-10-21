using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.comum.enums;
using multiplixe.empresas.client;
using multiplixe.usuarios.perfil.access_token;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil
{
    public class AccessTokenServico
    {
        private readonly PerfilServico perfilServico;
        private readonly EmpresaClient empresaClient;

        public AccessTokenServico(PerfilServico perfilServico, EmpresaClient empresaClient)
        {
            this.perfilServico = perfilServico;
            this.empresaClient = empresaClient;
        }

        public ResponseEnvelope<comum_dto.PerfilAccessToken> Obter(Guid usuariosId, RedeSocialEnum redesocial)
        {
            var response = new ResponseEnvelope<comum_dto.PerfilAccessToken>();

            var responsePerfil = perfilServico.Obter(new Filtro
            {
                UsuarioId = usuariosId,
                RedeSocial = redesocial
            });

            responsePerfil.ThrownIfError();

            response.HttpStatusCode = responsePerfil.HttpStatusCode;

            var perfil = responsePerfil.Item;

            if (string.IsNullOrEmpty(perfil.Token))
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                var servicoRedeSocial = (new Factory(redesocial, perfil.EmpresaId, empresaClient)).Obter();

                response.Item = servicoRedeSocial.Parse(perfil.Token);
            }

            return response;
        }
    }
}

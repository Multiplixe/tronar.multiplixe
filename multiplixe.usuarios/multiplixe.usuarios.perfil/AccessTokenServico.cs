using adduo.helper.envelopes;
using multiplixe.comum.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.usuarios.perfil
{
    public class AccessTokenServico
    {
        private readonly PerfilServico perfilServico;

        public AccessTokenServico(PerfilServico perfilServico)
        {
            this.perfilServico = perfilServico;
        }

        public ResponseEnvelope<string> Obter(Guid usuariosId, RedeSocialEnum redesocial)
        {
            var response = new ResponseEnvelope<string>();

            var responsePerfil = perfilServico.Obter(new Filtro
            {
                UsuarioId = usuariosId,
                RedeSocial = redesocial
            });

            response.HttpStatusCode = responsePerfil.HttpStatusCode;

            if (responsePerfil.Success)
            {
                var perfil = responsePerfil.Item;

                response.Item = perfil.Token;
            }

            return response;
        }
    }
}

using System;
using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.grpc.protos;

namespace multiplixe.usuarios.grpc.parsers
{
    public class UsuarioObter : UsuarioBase
    {
        public dto.filtros.UsuarioFiltro Request(proto.UsuarioFiltroRequest usuarioFiltro)
        {
            var filtro = new dto.filtros.UsuarioFiltro
            {
                Email = usuarioFiltro.Email
            };

            Guid usuarioId = Guid.Empty;
            Guid empresaId = Guid.Empty;

            Guid.TryParse(usuarioFiltro.UsuarioId, out usuarioId);
            Guid.TryParse(usuarioFiltro.EmpresaId, out empresaId);

            filtro.UsuarioId = usuarioId;
            filtro.EmpresaId = empresaId;


            return filtro;
        }

        public proto.UsuarioResponse Response(adduohelper.ResponseEnvelope<dto.Usuario> envelope)
        {
            var response = new proto.UsuarioResponse
            {
                HttpStatusCode = (int)envelope.HttpStatusCode
            };

            if(envelope.Success)
            {
                response.Usuario = base.Response(envelope.Item);
            }

            return response;
        }
    }
}

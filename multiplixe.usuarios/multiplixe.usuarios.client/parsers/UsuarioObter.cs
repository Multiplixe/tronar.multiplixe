using System;
using System.Net;
using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.grpc.protos;

namespace multiplixe.usuarios.client.parsers
{
    class UsuarioObter : UsuarioBase
    {
        public proto.UsuarioFiltroRequest Request(adduohelper.RequestEnvelope<dto.filtros.UsuarioFiltro> request)
        {
            return new proto.UsuarioFiltroRequest
            {
                UsuarioId = request.Item.UsuarioId.ToString(),
                EmpresaId = request.Item.EmpresaId.ToString(),
                Email = string.IsNullOrEmpty(request.Item.Email) ? string.Empty : request.Item.Email
            };
        }

        public adduohelper.ResponseEnvelope<dto.Usuario> Response(proto.UsuarioResponse message)
        {
            var response = new adduohelper.ResponseEnvelope<dto.Usuario>
            {
                HttpStatusCode = (HttpStatusCode)message.HttpStatusCode
            };

            if (response.Success)
            {
                response.Item = base.Response(message.Usuario);
            }

            return response;
        }
    }
}

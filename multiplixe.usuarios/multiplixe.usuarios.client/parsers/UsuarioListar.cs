using System;
using System.Collections.Generic;
using System.Net;
using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.grpc.protos;

namespace multiplixe.usuarios.client.parsers
{
    class UsuarioListar : UsuarioBase
    {
        public proto.UsuarioFiltroRequest Request(adduohelper.RequestEnvelope<dto.filtros.UsuarioFiltro> envelope)
        {
            var request = new proto.UsuarioFiltroRequest();

            foreach (var id in envelope.Item.UsuariosIdLista)
            {
                request.UsuariosIdLista.Add(id.ToString());
            }

            return request;
        }

        public adduohelper.ResponseEnvelope<List<dto.Usuario>> Response(proto.UsuarioResponse message)
        {
            var response = new adduohelper.ResponseEnvelope<List<dto.Usuario>>
            {
                HttpStatusCode = (HttpStatusCode)message.HttpStatusCode
            };

            if (response.Success)
            {
                foreach (var usuario in message.Usuarios)
                {
                    response.Item.Add(base.Response(usuario));
                }
            }

            return response;
        }
    }
}

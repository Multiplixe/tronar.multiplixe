using System;
using System.Collections.Generic;
using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.grpc.protos;

namespace multiplixe.usuarios.grpc.parsers
{
    public class UsuarioListar : UsuarioBase
    {
        public dto.filtros.UsuarioFiltro Request(proto.UsuarioFiltroRequest usuarioFiltro)
        {
            var filtro = new dto.filtros.UsuarioFiltro();

            foreach (var id in usuarioFiltro.UsuariosIdLista)
            {
                filtro.UsuariosIdLista.Add(Guid.Parse(id));
            }

            return filtro;
        }

        public proto.UsuarioResponse Response(adduohelper.ResponseEnvelope<List<dto.Usuario>> envelope)
        {
            var response = new proto.UsuarioResponse
            {
                HttpStatusCode = (int)envelope.HttpStatusCode
            };

            if(envelope.Success)
            {
                foreach (var dto in envelope.Item)
                {
                    response.Usuarios.Add(base.Response(dto));
                }
            }

            return response;
        }
    }
}

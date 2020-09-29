using adduohelper = adduo.helper;
using proto = multiplixe.usuarios.grpc.protos;
using dto = multiplixe.comum.dto;
using System.Net;

namespace multiplixe.usuarios.client.parsers
{
    class UsuarioAtualizar
    {
        public proto.UsuarioAtualizacaoRequest Request(adduohelper.envelopes.RequestEnvelope<dto.entries.UsuarioAtualizacao> request)
        {
            return new proto.UsuarioAtualizacaoRequest
            {
                Id = request.Item.UsuarioId.ToString(),
                EmpresaId = request.Item.EmpresaId.ToString(),
                Nome = new proto.EntryString
                {
                    Value = request.Item.Nome.Value ?? string.Empty
                },
                Apelido = new proto.EntryString
                {
                    Value = request.Item.Apelido.Value ?? string.Empty
                },
                Email = new proto.EntryString
                {
                    Value = request.Item.Email.Value ?? string.Empty
                }
            };
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.entries.UsuarioAtualizacao> Response(proto.UsuarioAtualizacaoResponse usuarioResponse)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.entries.UsuarioAtualizacao>
            {
                HttpStatusCode = (HttpStatusCode)usuarioResponse.HttpStatusCode,
                Item = new dto.entries.UsuarioAtualizacao
                {
                    Nome = new adduohelper.entries.Name
                    {
                        Code = (adduohelper.entries.CODE)usuarioResponse.Usuario.Nome.Code,
                        Status = (adduohelper.entries.STATUS)usuarioResponse.Usuario.Nome.Status,
                        Value = usuarioResponse.Usuario.Nome.Value
                    },
                    Apelido = new adduohelper.entries.String32
                    {
                        Code = (adduohelper.entries.CODE)usuarioResponse.Usuario.Apelido.Code,
                        Status = (adduohelper.entries.STATUS)usuarioResponse.Usuario.Apelido.Status,
                        Value = usuarioResponse.Usuario.Apelido.Value
                    },
                    Email = new adduohelper.entries.Email
                    {
                        Code = (adduohelper.entries.CODE)usuarioResponse.Usuario.Email.Code,
                        Status = (adduohelper.entries.STATUS)usuarioResponse.Usuario.Email.Status,
                        Value = usuarioResponse.Usuario.Email.Value
                    } 
                }
            };

            return response;
        }
    }
}

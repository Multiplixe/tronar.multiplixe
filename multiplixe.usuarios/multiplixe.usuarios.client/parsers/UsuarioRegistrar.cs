using adduohelper = adduo.helper;
using proto = multiplixe.usuarios.grpc.protos;
using dto = multiplixe.comum.dto;
using System.Net;

namespace multiplixe.usuarios.client.parsers
{
    class UsuarioRegistrar
    {
        public proto.UsuarioRegistroRequest Request(adduohelper.envelopes.RequestEnvelope<dto.entries.UsuarioRegistro> request)
        {
            return new proto.UsuarioRegistroRequest
            {
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
                },
                Senha = new proto.EntryString
                {
                    Value = request.Item.Senha.Value ?? string.Empty
                },
                EmpresaId = request.Item.EmpresaId.ToString()
            };
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.entries.UsuarioRegistro> Response(proto.UsuarioRegistroResponse usuarioResponse)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.entries.UsuarioRegistro>
            {
                HttpStatusCode = (HttpStatusCode)usuarioResponse.HttpStatusCode,
                Item = new dto.entries.UsuarioRegistro
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
                    },
                    Senha = new adduohelper.entries.Password
                    {
                        Code = (adduohelper.entries.CODE)usuarioResponse.Usuario.Senha.Code,
                        Status = (adduohelper.entries.STATUS)usuarioResponse.Usuario.Senha.Status,
                        Value = string.Empty
                    }
                }
            };

            return response;
        }
    }
}

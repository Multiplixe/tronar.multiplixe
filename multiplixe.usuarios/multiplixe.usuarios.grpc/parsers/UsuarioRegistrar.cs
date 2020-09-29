using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.grpc.protos;
using System;
using adduohelper = adduo.helper.envelopes;

namespace multiplixe.usuarios.grpc.parsers
{
    public class UsuarioRegistrar
    {
        public adduohelper.RequestEnvelope<dto.entries.UsuarioRegistro> Request(proto.UsuarioRegistroRequest message)
        {
            var usuario = new dto.entries.UsuarioRegistro(
                                    message.Nome.Value,
                                    message.Apelido.Value,
                                    message.Email.Value,
                                    message.Senha.Value,
                                    Guid.Parse(message.EmpresaId));

            var request = new adduohelper.RequestEnvelope<dto.entries.UsuarioRegistro>(usuario);

            return request;
        }

        public proto.UsuarioRegistroResponse Response(adduohelper.ResponseEnvelope<dto.entries.UsuarioRegistro> envelope)
        {
            var response = new proto.UsuarioRegistroResponse
            {
                HttpStatusCode = (int)envelope.HttpStatusCode,
                Usuario = new proto.UsuarioRegistroRequest
                {
                    Nome = new proto.EntryString
                    {
                        Value = envelope.Item.Nome.Value,
                        Code = (int)envelope.Item.Nome.Code,
                        Status = (int)envelope.Item.Nome.Status
                    },
                    Apelido = new proto.EntryString
                    {
                        Value = envelope.Item.Apelido.Value,
                        Code = (int)envelope.Item.Apelido.Code,
                        Status = (int)envelope.Item.Apelido.Status
                    },
                    Email = new proto.EntryString
                    {
                        Value = envelope.Item.Email.Value,
                        Code = (int)envelope.Item.Email.Code,
                        Status = (int)envelope.Item.Email.Status
                    },
                    Senha = new proto.EntryString
                    {
                        Value = envelope.Item.Senha.Value,
                        Code = (int)envelope.Item.Senha.Code,
                        Status = (int)envelope.Item.Senha.Status
                    }
                }
            };

            return response;
        }

    }
}

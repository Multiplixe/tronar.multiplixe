using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.grpc.protos;
using System;
using adduohelper = adduo.helper.envelopes;

namespace multiplixe.usuarios.grpc.parsers
{
    public class UsuarioAtualizar
    {
        public adduohelper.RequestEnvelope<dto.entries.UsuarioAtualizacao> Request(proto.UsuarioAtualizacaoRequest message)
        {
            var usuario = new dto.entries.UsuarioAtualizacao(
                                    Guid.Parse(message.Id),
                                    Guid.Parse(message.EmpresaId),
                                    message.Nome.Value,
                                    message.Apelido.Value,
                                    message.Email.Value);

            var request = new adduohelper.RequestEnvelope<dto.entries.UsuarioAtualizacao>(usuario);

            return request;
        }

        public proto.UsuarioAtualizacaoResponse Response(adduohelper.ResponseEnvelope<dto.entries.UsuarioAtualizacao> envelope)
        {
            var response = new proto.UsuarioAtualizacaoResponse
            {
                HttpStatusCode = (int)envelope.HttpStatusCode,
                Usuario = new proto.UsuarioAtualizacaoRequest
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
                    }
                }
            };

            return response;
        }

    }
}

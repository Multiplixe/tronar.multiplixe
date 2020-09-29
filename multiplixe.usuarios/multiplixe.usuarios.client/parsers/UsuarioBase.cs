using adduo.helper.extensionmethods;
using multiplixe.usuarios.grpc.protos;
using System;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.client.parsers
{
    public class UsuarioBase
    {
        public dto.Usuario Response(UsuarioMessage proto)
        {
            return new dto.Usuario
            {
                Id = proto.Id.ToGuid(),
                Nome = proto.Nome,
                Apelido = proto.Apelido,
                Email = proto.Email,
                DataCadastro = new DateTime(proto.DataCadastro),
                EmpresaId = proto.EmpresaId.ToGuid()
            };
        }
    }
}

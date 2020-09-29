using multiplixe.classificador.grpc.Protos;
using System;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.grpc.parsers
{
    public class UsuarioRegistrar
    {
        public dto.Usuario Request(UsuarioRequest usuarioMessage)
        {
            return new dto.Usuario
            {
                Id = Guid.Parse(usuarioMessage.Id),
                EmpresaId = Guid.Parse(usuarioMessage.EmpresaId)
            };
        }
    }
}

using multiplixe.classificador.grpc.Protos;
using System;

namespace multiplixe.classificador.grpc.parsers
{
    public class UsuarioDeletar
    {
        public Guid Request(UsuarioRequest usuarioMessage)
        {
            return Guid.Parse(usuarioMessage.Id);
        }
    }
}

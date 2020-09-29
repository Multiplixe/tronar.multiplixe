using multiplixe.classificador.grpc.Protos;
using System;

namespace multiplixe.classificador.client.parsers
{
    public class UsuarioDeletar : UsuarioRegistrar
    {
        public virtual UsuarioRequest Request(Guid usuarioId)
        {
            var usuarioMessage = new UsuarioRequest()
            {
                Id = usuarioId.ToString(),
                EmpresaId = string.Empty
            };

            return usuarioMessage;
        }

    }
}

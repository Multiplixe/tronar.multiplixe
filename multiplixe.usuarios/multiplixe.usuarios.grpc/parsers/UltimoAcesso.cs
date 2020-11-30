using multiplixe.comum.dto;
using multiplixe.usuarios.grpc.protos;
using System;

namespace multiplixe.usuarios.grpc.parsers
{
    public class UltimoAcesso
    {
        public UsuarioUltimoAcesso Request(UltimoAcessoRequest request)
        {
            return new UsuarioUltimoAcesso
            {
                UsuarioId = Guid.Parse(request.UsuarioId),
                Acesso = new DateTime(request.Acesso)
            };
        }
    }
}

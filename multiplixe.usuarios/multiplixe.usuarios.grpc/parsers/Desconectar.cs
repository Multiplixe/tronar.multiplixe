using adduo.helper.extensionmethods;
using multiplixe.comum.enums;
using multiplixe.usuarios.perfil.grpc.Protos;

namespace multiplixe.usuarios.grpc.parsers
{
    public class Desconectar
    {
        public comum.dto.Perfil Request(DesconectarRequest request)
        {
            return new comum.dto.Perfil
            {
                UsuarioId = request.UsuarioId.ToGuid(),
                PerfilId = request.PerfilId,
                RedeSocial = (RedeSocialEnum)request.RedeSocial,
                Ativo = request.Ativo
            };
        }
    }
}

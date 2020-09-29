using adduo.helper.extensionmethods;
using multiplixe.usuarios.perfil.grpc.Protos;
using enums = multiplixe.comum.enums;

namespace multiplixe.usuarios.grpc.parsers
{
    public class PerfilObter
    {
        public perfil.Filtro Request(PerfilFiltro filtro)
        {
            return new perfil.Filtro
            {
                EmpresaId = filtro.EmpresaId.ToGuid(),
                UsuarioId = filtro.UsuarioId.ToGuid(),
                PerfilId = filtro.PerfilId,
                RedeSocial = (enums.RedeSocialEnum)filtro.RedeSocial
            };
        }
    }
}

using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.avatar.console.services
{
    public class PathHelper
    {
        public AppSettings settings { get; }

        public PathHelper(AppSettings settings)
        {
            this.settings = settings;
        }
        public string CriarImagemCaminhoCompleto(dto.AvatarParaProcessar avatarParaProcessar)
        {
            return $"{avatarParaProcessar.Caminho}/{avatarParaProcessar.Avatar.Imagem}";
        }
    }
}

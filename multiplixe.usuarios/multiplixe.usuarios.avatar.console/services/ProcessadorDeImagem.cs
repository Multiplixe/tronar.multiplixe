using multiplixe.comum.helper;
using System;
using System.IO;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.avatar.console.services
{
    public class ProcessadorDeImagem
    {
        private AppSettings settings { get; }
        private PathHelper pathHelper { get; }

        public ProcessadorDeImagem(AppSettings settings, PathHelper pathHelper)
        {
            this.settings = settings;
            this.pathHelper = pathHelper;
        }

        public void Processar(dto.AvatarParaProcessar avatarParaProcessar)
        {
            Console.WriteLine("Processou", avatarParaProcessar.Avatar.Imagem);
        }

        public void Delete(dto.AvatarParaProcessar avatarParaProcessar)
        {
            var path = pathHelper.CriarImagemCaminhoCompleto(avatarParaProcessar);

            if (File.Exists(path))
            {
                System.IO.File.Delete(path);
                Console.WriteLine("Deletou {0}", path);
            }
        }
    }
}

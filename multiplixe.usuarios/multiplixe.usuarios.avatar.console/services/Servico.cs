using multiplixe.central_rtdb.client;
using System;
using System.IO;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.avatar.console.services
{
    public class Servico
    {
        private AppSettings settings { get; }
        private PathHelper pathHelper { get; }
        private ProcessadorDeImagem processadorDeImagem { get; }
        private Firebase firebase { get; }
        private RTDBAtividadeClient rtdbAtividadeClient { get; }

        public Servico(AppSettings settings,
            PathHelper pathHelper,
            ProcessadorDeImagem processadorDeImagem,
            Firebase firebase,
            RTDBAtividadeClient rtdbAtividadeClient)
        {
            this.settings = settings;
            this.pathHelper = pathHelper;
            this.processadorDeImagem = processadorDeImagem;
            this.firebase = firebase;
            this.rtdbAtividadeClient = rtdbAtividadeClient;
        }

        public async void Processar(dto.AvatarParaProcessar avatarParaProcessar)
        {
            var imagemCaminhoCompleto = pathHelper.CriarImagemCaminhoCompleto(avatarParaProcessar);

            if (File.Exists(imagemCaminhoCompleto))
            {
                processadorDeImagem.Processar(avatarParaProcessar);

                await firebase.Processar(avatarParaProcessar);

                rtdbAtividadeClient.RegistrarAvatar(avatarParaProcessar);

                processadorDeImagem.Delete(avatarParaProcessar);
            }
            else
            {
                Console.WriteLine("Imagem não encontrada {0}", imagemCaminhoCompleto);
            }
        }
    }
}

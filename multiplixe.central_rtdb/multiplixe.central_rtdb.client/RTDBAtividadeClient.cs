using multiplixe.comum.helper;
using multiplixe.central_rtdb.grpc.protos;
using System;
using System.Net;
using dto = multiplixe.comum.dto;

namespace multiplixe.central_rtdb.client
{
    public class RTDBAtividadeClient : BaseClient
    {
        private parsers.Atividade atividadeParser { get; }
        
        public RTDBAtividadeClient()
        {
            atividadeParser = new parsers.Atividade();
        }

        public void RegistrarClassificacao(Guid usuarioId)
        {
            var request = atividadeParser.Request(usuarioId, "score", ObterGatilho());

            Registrar(request);
        }


        public void RegistrarAvatar(dto.AvatarParaProcessar avatarParaProcessar)
        {
            var request = atividadeParser.Request(avatarParaProcessar.UsuarioId, "avatar", new { timestamp = avatarParaProcessar.Avatar.Timestamp });

            Registrar(request);
        }

        public void RegistrarPerfil(Guid usuarioId)
        {
            var request = atividadeParser.Request(usuarioId, "connection", ObterGatilho());

            Registrar(request);
        }

        private void Registrar(AtividadeRequest request)
        {
            var response = client.RegistrarAtividade(request);

            if (response.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new System.Exception($"Erro ao registrar atividade no RTDB -> {response.Erro}");
            }
        }

    }
}

using multiplixe.comum.helper;
using multiplixe.central_rtdb.grpc.protos;
using System;
using System.Net;
using dto = multiplixe.comum.dto;

namespace multiplixe.central_rtdb.client
{
    public class RTDBAtividadeComumClient : BaseComumClient
    {
        private parsers.AtividadeComum atividadeParser { get; }
        
        public RTDBAtividadeComumClient()
        {
            atividadeParser = new parsers.AtividadeComum();
        }

        public void RegistrarRanking()
        {
            var request = atividadeParser.Request("ranking", ObterGatilho());

            Registrar(request);
        }

        private void Registrar(AtividadeRequest request)
        {
            var response = client.RegistrarAtividadeComum(request);

            if (response.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new System.Exception($"Erro ao registrar atividade no RTDB -> {response.Erro}");
            }
        }
    }
}

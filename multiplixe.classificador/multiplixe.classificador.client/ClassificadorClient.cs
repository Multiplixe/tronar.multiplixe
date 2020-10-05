using multiplixe.classificador.grpc.Protos;
using System;
using comum_dto = multiplixe.comum.dto;
using envelopes = adduo.helper.envelopes;

namespace multiplixe.classificador.client
{
    public class ClassificadorClient : BaseClient
    {
        private Classificador.ClassificadorClient client { get; set; }

        public ClassificadorClient()
        {
            client = new Classificador.ClassificadorClient(channel);
        }

        public envelopes.ResponseEnvelope<comum_dto.classificacao.Classificacao> ObterClassificacao(Guid usuarioId)
        {
            var parser = new parsers.ObterClassificacao();

            var request = parser.Request(usuarioId);

            var classificacaoResponse = client.ObterClassificacao(request);

            var responseEnvelope = parser.Response(classificacaoResponse);

            return responseEnvelope;
        }
    }
}

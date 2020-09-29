using multiplixe.classificador.grpc.Protos;
using System;
using coredto = multiplixe.comum.dto;
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

        public envelopes.ResponseEnvelope<coredto.classificacao.Classificacao> ObterClassificacao(Guid usuarioId)
        {
            var parser = new parsers.ObterClassificacao();

            var request = parser.Request(usuarioId);

            var classificacaoResponse = client.ObterClassificacao(request);

            var responseEnvelope = parser.Response(classificacaoResponse);

            return responseEnvelope;
        }

        public envelopes.ResponseEnvelope<coredto.classificacao.Pontuacao> ObterPontuacaoTotal(Guid usuarioId)
        {
            var parser = new parsers.ObterPontuacaoTotal();

            var request = parser.Request(usuarioId);

            var pontuacaoResponse = client.ObterPontuacaoTotal(request);

            var responseEnvelope = parser.Response(pontuacaoResponse);

            return responseEnvelope;
        }
    }
}

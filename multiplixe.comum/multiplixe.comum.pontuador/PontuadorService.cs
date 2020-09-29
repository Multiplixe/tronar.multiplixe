using coredto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;
using enfileiradorgrpc = multiplixe.enfileirador.client;

namespace multiplixe.comum.pontuador
{
    public class PontuadorService<T> : coreinterfaces.pontuador.IPontuadorService<T>  where T : coredto.EventoBase
    {
        private enfileiradorgrpc.EnfileiradorClient enfileiradorClient { get; }
        public coreinterfaces.pontuador.IPontuador<T> pontuador { get; }

        public PontuadorService(
            enfileiradorgrpc.EnfileiradorClient enfileiradorClient,
            coreinterfaces.pontuador.IPontuador<T> pontuador)
        {
            this.enfileiradorClient = enfileiradorClient;
            this.pontuador = pontuador;
        }

        public void ProcessarEvento(coredto.EnvelopeEvento<T> envelope)
        {
            var pontuacao = Pontuar(envelope);

            Enfileirar(pontuacao);
        }

        private coredto.Ponto Pontuar(coredto.EnvelopeEvento<T> envelope)
        {
            return this.pontuador.Pontuar(envelope);
        }

        private void Enfileirar(coredto.Ponto ponto)
        {
            if (!ponto.Pontos.Equals(0))
            {
                this.enfileiradorClient.EnfileirarParaPontuador(ponto);
            }
        }
    }
}

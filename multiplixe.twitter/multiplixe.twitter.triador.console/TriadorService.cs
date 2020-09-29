using multiplixe.comum.triador;
using multiplixe.twitter.dto.eventos;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.twitter.triador.console
{
    public class TriadorService
    {
        private TriadorService<EventoReacao> triadorService { get; set; }

        public TriadorService(
            TriadorService<EventoReacao> triadorService)
        {
            this.triadorService = triadorService;
        }

        public void ProcessarEnvelope(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            ValidarEvento(envelope);

            if (envelope.Evento.favorite_events != null)
            {
                foreach (var reacao in envelope.Evento.favorite_events)
                {
                    var envelopeReacao = envelope.Transformar<EventoReacao>(reacao);

                    triadorService.ProcessarEnvelope(envelopeReacao);
                }
            }
        }

        private void ValidarEvento(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            if (envelope.Evento == null)
            {
                throw new System.Exception("Evento vazio");
            }
        }
    }
}

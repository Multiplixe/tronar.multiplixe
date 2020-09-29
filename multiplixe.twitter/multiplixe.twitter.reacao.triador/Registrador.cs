using multiplixe.twitter.dto.eventos;
using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;
using registrador = multiplixe.registrador_de_eventos.client.twitter;

namespace multiplixe.twitter.reacao.triador
{
    public class Registrador :
        coreinterfaces.triador.IRegistradorEventoTriagem<EventoReacao>,
        coreinterfaces.IRegistradorEventosConsultas<EventoReacao>
    {
        public registrador.Client twitterClient { get; }

        public Registrador(
            registrador.Client twitterClient)
        {
            this.twitterClient = twitterClient;
        }

        public void RegistrarEvento(comum_dto.EnvelopeEvento<EventoReacao> envelope)
        {
            var facade = new EventoReacaoFacade(envelope.Evento);

            twitterClient.RegistrarReacao(envelope.Id, envelope.UsuarioId, facade.PerfilId, facade.PostId, envelope.DataEvento, envelope.Evento, facade.Tipo);
        }

        public comum_dto.Reacao ObterUltimaReacao(comum_dto.EnvelopeEvento<EventoReacao> envelope)
        {
            var dto = new EventoReacaoFacade(envelope.Evento);

            var response = twitterClient.ObterUltimaReacao(envelope.UsuarioId, dto.PostId);

            var evento = response.Item;

            return evento;
        }
    }
}

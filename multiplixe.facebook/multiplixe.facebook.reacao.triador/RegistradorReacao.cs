using comum_dto = multiplixe.comum.dto;
using registrador = multiplixe.registrador_de_eventos.client.facebook;
using coreinterfaces = multiplixe.comum.interfaces;
using multiplixe.facebook.dto.eventos;

namespace multiplixe.facebook.reacao.triador
{
    public class RegistradorReacao :
           coreinterfaces.triador.IRegistradorEventoTriagem<Evento>,
           coreinterfaces.IRegistradorEventosConsultas<Evento>
    {
        public registrador.Client facebookClient { get; }

        public RegistradorReacao(
            registrador.Client facebookClient)
        {
            this.facebookClient = facebookClient;
        }

        public void RegistrarEvento(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            var dto = new EventoFacade(envelope.Evento);

            facebookClient.RegistrarReacao(
                envelope.Id,
                envelope.UsuarioId,
                dto.PerfilId,
                dto.PostId,
                envelope.DataEvento,
                envelope.Evento,
                dto.Intensidade,
                dto.Tipo);
        }

        public comum_dto.Reacao ObterUltimaReacao(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            var dto = new EventoFacade(envelope.Evento);

            var response = facebookClient.ObterUltimaReacao(envelope.UsuarioId, dto.PostId);

            var evento = response.Item;

            return evento;
        }
    }
}

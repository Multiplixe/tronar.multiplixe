using adduo.helper.envelopes;
using multiplixe.twitter.dto.eventos;
using multiplixe.usuarios.client;
using comum_dto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.twitter.reacao.triador
{
    public class IdentificadorUsuario : coreinterfaces.triador.IIdentificadorUsuario<EventoReacao>
    {
        private PerfilClient client { get; }

        public IdentificadorUsuario(PerfilClient client)
        {
            this.client = client;
        }

        public ResponseEnvelope<comum_dto.Perfil> Identificar(comum_dto.EnvelopeEvento<EventoReacao> envelope)
        {
            var dto = new EventoReacaoFacade(envelope.Evento);
            return client.Obter(envelope.EmpresaId, coreenums.RedeSocialEnum.twitter, dto.PerfilId);
        }
    }
}

using adduohelper = adduo.helper;
using comum_dto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using coreinterfaces = multiplixe.comum.interfaces;
using multiplixe.facebook.dto.eventos;
using multiplixe.usuarios.client;

namespace multiplixe.facebook.triador.console
{
    public class IdentificadorUsuario : coreinterfaces.triador.IIdentificadorUsuario<Evento>
    {
        private PerfilClient client { get; }

        public IdentificadorUsuario(PerfilClient client)
        {
            this.client = client;
        }

        public adduohelper.envelopes.ResponseEnvelope<comum_dto.Perfil> Identificar(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            var eventoFacade = new EventoFacade(envelope.Evento);

            return client.Obter(envelope.EmpresaId, coreenums.RedeSocialEnum.facebook, eventoFacade.PerfilId);
        }
    }
}

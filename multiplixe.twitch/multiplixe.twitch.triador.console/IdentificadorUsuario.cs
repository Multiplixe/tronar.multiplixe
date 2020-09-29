using multiplixe.usuarios.client;
using multiplixe.twitch.dto.eventos;
using adduohelper = adduo.helper;
using comum_dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;
using multiplixe.comum.interfaces.triador;

namespace multiplixe.twitch.triador.console
{
    public class IdentificadorUsuario : IIdentificadorUsuario<Evento>
    {
        private PerfilClient client { get; }

        public IdentificadorUsuario(PerfilClient client)
        {
            this.client = client;
        }

        public adduohelper.envelopes.ResponseEnvelope<comum_dto.Perfil> Identificar(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            return client.Obter(envelope.EmpresaId, enums.RedeSocialEnum.twitch, envelope.Evento.PerfilId);
        }
    }
}

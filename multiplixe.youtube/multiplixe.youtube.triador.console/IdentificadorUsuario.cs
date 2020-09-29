using multiplixe.usuarios.client;
using adduohelper = adduo.helper;
using coredto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.youtube.triador.console
{
    public class IdentificadorUsuario : coreinterfaces.triador.IIdentificadorUsuario<dto.eventos.Evento>
    {
        private PerfilClient client { get; }

        public IdentificadorUsuario(PerfilClient client)
        {
            this.client = client;
        }

        public adduohelper.envelopes.ResponseEnvelope<coredto.Perfil> Identificar(coredto.EnvelopeEvento<dto.eventos.Evento> envelope)
        {
            return  client.Obter(envelope.EmpresaId, coreenums.RedeSocialEnum.youtube, envelope.Evento.PerfilId);
        }
    }
}

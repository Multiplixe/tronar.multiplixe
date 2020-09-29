using multiplixe.comum.dto;
using multiplixe.comum.interfaces.triador;
using multiplixe.registrador_de_eventos.client.youtube;
using multiplixe.youtube.dto.eventos;

namespace multiplixe.youtube.livehashtag.triador
{
    public class Avaliador : IAvaliadorDeEvento<dto.eventos.LiveHashtag>
    {
        public bool Avaliar(EnvelopeEvento<LiveHashtag> envelope)
        {
            var registrador = new Client();

            var existe = registrador.VerificarExistenciaLiveHashtag(envelope.UsuarioId, envelope.Evento.PostId);

            return !existe;
        }
    }
}

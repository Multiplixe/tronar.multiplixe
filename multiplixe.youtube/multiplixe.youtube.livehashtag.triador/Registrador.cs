using multiplixe.comum.dto;
using multiplixe.registrador_de_eventos.client.youtube;
using multiplixe.youtube.dto.eventos;
using System;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.youtube.livehashtag.triador
{
    public class Registrador : coreinterfaces.triador.IRegistradorEventoTriagem<dto.eventos.LiveHashtag>
    {
        public void RegistrarEvento(EnvelopeEvento<LiveHashtag> envelope)
        {
            try
            {
                var client = new Client();

                client.RegistrarLiveHashtag(envelope.Id, envelope.UsuarioId, envelope.Evento.PostId, envelope.Evento.PerfilId, envelope.DataEvento, envelope.Evento.Hashtag);
            }
            catch (Exception ex)
            {
                throw ex;
                // ## TODO log
            }
        }
    }
}

using System;
using multiplixe.comum.enums;

namespace multiplixe.comum.dto
{
    public class Ponto
    {
        public Guid EventoId { get; set; }

        public Guid EmpresaId { get; set; }

        public Guid UsuarioId { get; set; }

        public string PostId { get; set; }

        public string PerfilId { get; set; }

        public DateTime DataEvento { get; set; }

        public DateTime DataPontuacao { get; set; }

        public TipoEventoEnum TipoEvento { get; set; }

        public int Pontos { get; set; }

        public RedeSocialEnum RedeSocial { get; set; }

        public Ponto(Guid eventoId, Guid usuarioId, Guid empresaId, string postId, string perfilId, DateTime dataEvento, DateTime dataPontuacao, TipoEventoEnum tipoEvento,  int pontos, RedeSocialEnum redeSocialEnum)
        {
            EventoId = eventoId;
            UsuarioId = usuarioId;
            EmpresaId = empresaId;
            PostId = postId;
            PerfilId = perfilId;
            DataEvento = dataEvento;
            DataPontuacao = dataPontuacao;
            TipoEvento = tipoEvento;
            Pontos = pontos;
            RedeSocial = redeSocialEnum;
        }

        public Ponto()
        {

        }

    }
}

using adduo.helper.envelopes;
using multiplixe.classificador.client;
using multiplixe.usuarios.client;
using System;
using System.Linq;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.consultas
{
    public class Ranking
    {
        private RankingClient rankingClient { get; }
        private UsuarioClient usuarioClient { get; }

        public Ranking(RankingClient rankingClient, UsuarioClient usuarioClient)
        {
            this.rankingClient = rankingClient;
            this.usuarioClient = usuarioClient;
        }

        public adduohelper.ResponseEnvelope<comum_dto.ranking.Ranking> Obter(Guid usuarioId)
        {
            var rankingResponse = rankingClient.Obter(usuarioId);

            if (rankingResponse.Success)
            {
                var requestUsuario = new RequestEnvelope<comum_dto.filtros.UsuarioFiltro>();
                requestUsuario.Item = new comum_dto.filtros.UsuarioFiltro() {
                    UsuariosIdLista = rankingResponse.Item.Posicoes.Select(s => s.UsuarioId).ToList()
                };

                var usuariosResponse = usuarioClient.Listar(requestUsuario);

                if(usuariosResponse.Success)
                {
                    var usuarios = usuariosResponse.Item;

                    foreach (var posicao in rankingResponse.Item.Posicoes)
                    {
                        var usuario = usuarios.First(f => f.Id == posicao.UsuarioId);

                        posicao.Apelido = usuario.Apelido;
                        posicao.Nome = usuario.Nome;
                    }
                }
            }

            return rankingResponse;
        }

    }
}
 
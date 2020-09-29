using multiplixe.comum.dto.ranking;
using System.Linq;
using static Dapper.SqlMapper;
using coredapper = multiplixe.comum.dapper;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.ranking.extractor
{
    public class Obter : coredapper.IMultiResultExtractor
    {
        private Ranking ranking { get; }

        public Obter(dto.ranking.Ranking ranking)
        {
            this.ranking = ranking;
        }

        public void Execute(object o)
        {
            var result = (GridReader)o;

            var posicoes = result.Read<results.Posicao>();
            var controle = result.Read<results.Controle>();

            ranking.Posicoes = posicoes.Select(s => new Posicao {
                UsuarioId = s.UsuarioId,
                Pontos = s.Pontos,
                Valor = s.Valor
            }).ToList();

            if (controle.Any())
            {
                ranking.DataProcessamento = controle.First().DataProcessamento;
            }
        }
    }
}

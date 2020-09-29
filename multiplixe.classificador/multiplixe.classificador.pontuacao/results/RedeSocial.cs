using multiplixe.comum.dapper;

namespace multiplixe.classificador.pontuacao.results
{
    public class RedeSocial : BaseResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Pontos { get; set; }
    }
}

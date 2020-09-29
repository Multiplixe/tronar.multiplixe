using multiplixe.comum.dapper;

namespace multiplixe.classificador.nivel.results
{
    public class Nivel : BaseResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int PontuacaoMinima { get; set; }
    }
}

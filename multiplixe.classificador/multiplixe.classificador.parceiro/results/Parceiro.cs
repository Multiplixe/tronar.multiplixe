using multiplixe.comum.dapper;
using System;

namespace multiplixe.classificador.parceiro.results
{
    public class Parceiro : BaseResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Status { get; set; }
    }
}

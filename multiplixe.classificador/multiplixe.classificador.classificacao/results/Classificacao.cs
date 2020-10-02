using multiplixe.comum.dapper;
using System;

namespace multiplixe.classificador.classificacao.results
{
    public class Classificacao : BaseResult
    {
        public Guid UsuarioId { get; set; }
        public Guid EmpresaId { get; set; }
        public int Pontos { get; set; }
        public int Saldo { get; set; }
        public int NivelId { get; set; }
        public string Nivel { get; set; }
    }

}

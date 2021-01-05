using multiplixe.comum.dapper;
using System;

namespace multiplixe.compartilhador.compartilhamento.results
{
    public class Compartilhamento : BaseResult
    {
        public int Id { get; set; }
        public Guid EmpresaId { get; set; }
        public DateTime Atualizacao { get; set; }
        public bool Ativo { get; set; }
        public int Tipo { get; set; }
        public int Origem { get; set; }
        public string Formato { get; set; }
        public string Url { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}

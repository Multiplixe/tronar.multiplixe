using multiplixe.comum.dapper;
using multiplixe.comum.enums;
using System;

namespace multiplixe.classificador.transacao.results
{
    public class Transacao : BaseResult
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid ParceiroId { get; set; }
        public string ParceiroTransacaoId { get; set; }
        public int Pontos { get; set; }
        public DateTime Data { get; set; }
        public SaldoTransacaoTipoEnum Tipo { get; set; }
    }
}

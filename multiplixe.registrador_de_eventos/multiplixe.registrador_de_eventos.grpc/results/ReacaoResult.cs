using multiplixe.comum.dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace multiplixe.registrador_de_eventos.grpc.results
{
    public class ReacaoResult : BaseResult
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string PostId { get; set; }
        public string PerfilId { get; set; }
        public int Tipo { get; set; }
        public string Intensidade { get; set; }
    }
}

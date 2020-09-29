using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class UsuarioParaProcessar
    {
        public Guid UsuarioId { get; set; }
        public Guid EmpresaId { get; set; }
        public int Tentativa { get; set; }

        public UsuarioParaProcessar()
        {

        }

        public UsuarioParaProcessar(Guid usuarioId, int tentativa = 0)
        {
            UsuarioId = usuarioId;
            Tentativa = tentativa;
        }
        public UsuarioParaProcessar(Guid usuarioId, Guid empresaId, int tentativa = 0) : this(usuarioId, tentativa)
        {
            EmpresaId = empresaId;
        }
    }
}

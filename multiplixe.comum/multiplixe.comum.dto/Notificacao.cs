using multiplixe.comum.enums;
using System;
using System.Collections.Generic;

namespace multiplixe.comum.dto
{
    public class Notificacao
    {
        public Guid EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Destinatario { get; set; }
        public string Titulo { get; set; }
        public List<string> Paragrafos { get; set; }
        public TipoNotificacaoEnum Tipo { get; set; }

        public Notificacao()
        {
            Paragrafos = new List<string>();
        }
    }
}

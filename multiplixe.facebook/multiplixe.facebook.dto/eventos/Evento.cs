using comum_dto = multiplixe.comum.dto;
using System.Collections.Generic;

namespace multiplixe.facebook.dto.eventos
{
    public class Evento : comum_dto.EventoBase
    {
        public string @object { get; set; }
        
        public List<Entry> entry { get; set; }
    }
}

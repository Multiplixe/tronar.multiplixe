using System.Collections.Generic;

namespace multiplixe.facebook.dto.eventos
{
    public class Entry
    {
        public string id { get; set; }

        public int time { get; set; }

        public List<Change> changes { get; set; }
    }

}

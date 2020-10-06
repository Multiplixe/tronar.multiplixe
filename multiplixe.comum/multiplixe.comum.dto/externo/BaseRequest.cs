using System;

namespace multiplixe.comum.dto.externo
{
    public class BaseRequest
    {
        protected Guid TryParse(string id)
        {
            var guid = Guid.Empty;

            Guid.TryParse(id, out guid);

            return guid;
        }
    }
}

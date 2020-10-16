using multiplixe.comum.dto.interfaces;
using multiplixe.comum.enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Compartilhamento : IEmpresaID, IUsuarioID
    {
        public Guid EmpresaId { get; set; }
        public Guid UsuarioId { get; set; }

        [JsonPropertyName("postId")]
        public string PostId { get; set; }

        [JsonPropertyName("socialMedias")]
        public List<RedeSocialEnum> RedesSociais { get; set; }

        public Compartilhamento()
        {
            RedesSociais = new List<RedeSocialEnum>();
        }
    }
}

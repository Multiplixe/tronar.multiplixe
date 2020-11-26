using multiplixe.comum.dapper;
using multiplixe.publicidade_banner.grpc.Protos;
using System;

namespace multiplixe.publicidade_banner.grpc.results
{
    public class Banner : BaseResult
    {
        public Guid Id { get; set; }
        public string URL { get; set; }
        public int StatusId { get; set; }

        public BannerMessage ToMessage()
        {
            return new BannerMessage
            {
                Id = this.Id.ToString(),
                URL = this.URL,
                StatusId = this.StatusId
            };
        }
    }
}

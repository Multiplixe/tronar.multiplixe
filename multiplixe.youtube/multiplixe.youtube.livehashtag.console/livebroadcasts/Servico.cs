using adduo.helper.envelopes;
using multiplixe.comum.helper;
using  multiplixe.youtube.livehashtag.console.livebroadcasts.dtos;
using System.Collections.Generic;
using System.Net;

namespace  multiplixe.youtube.livehashtag.console.livebroadcasts
{
    public class Servico : BaseService
    {
        public Servico(AppSettings settings, oauth2.Servico oauth2Servico) : base(settings, oauth2Servico)
        {
        }

        public ResponseEnvelope<Item> ObterLive()
        {
            var url = string.Format(settings.url.liveBroadcasts, settings.apiKey);

            var token = oauth2Servico.ObterToken();

            var response = WebRequestHelper.GetExterno<Response>(url, ObterHeader());
            response.ThrownIfError();

            var envelope = new ResponseEnvelope<Item>();

            if (response.Item.PageInfo.TotalResults >= 1)
            {
                envelope.Item = response.Item.Items[0];
            }
            else
            {
                envelope.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return envelope;
        }
    }
}

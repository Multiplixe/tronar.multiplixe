using Grpc.Net.Client;
using multiplixe.comum.enums;
using System.Net.Http;

namespace multiplixe.comum.helper.grpc
{
    public class GrcpChannelHelper
    {

        public static GrpcChannel CreateChannel(PortasServicosEnum porta)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.Timeout = new System.TimeSpan(0, 0, 59);
            return GrpcChannel.ForAddress($"https://localhost:{(int)porta}", new GrpcChannelOptions { HttpClient = httpClient });
        }

    }
}

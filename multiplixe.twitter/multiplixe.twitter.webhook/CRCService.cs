using multiplixe.empresas.client;
using multiplixe.twitter.dto;
using System;
using System.Security.Cryptography;
using System.Text;

namespace multiplixe.twitter.webhook
{
    public class CRCService
    {
        private readonly EmpresaClient empresaClient;

        public CRCService(EmpresaClient empresaClient)
        {
            this.empresaClient = empresaClient;
        }

        public string ProcessarCRC(string crc, Guid empresaId, string contaRedeSocial)
        {
            if (string.IsNullOrEmpty(crc))
            {
                throw new ArgumentNullException("CRC");
            }

            var twitterInfoResponse = empresaClient.ObterInfoTwitter(empresaId, contaRedeSocial);

            twitterInfoResponse.ThrownIfError();

            var twitterInfo = twitterInfoResponse.Item;

            var encoding = new ASCIIEncoding();
            var key = encoding.GetBytes(twitterInfo.ConsumerSecret);
            var crc_tokenBytes = encoding.GetBytes(crc);

            using (HMACSHA256 hMACSHA256 = new HMACSHA256(key))
            {
                var hash = hMACSHA256.ComputeHash(crc_tokenBytes);

                var response = string.Format("sha256={0}", Convert.ToBase64String(hash));

                return response;
            }
        }
    }
}

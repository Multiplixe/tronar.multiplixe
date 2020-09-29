using System;
using System.Security.Cryptography;
using System.Text;

namespace multiplixe.comum.helper
{
    public class CriptografiaHelper
    {
        public static string Criptografar(string text, string key)
        {
            var retorno = string.Empty;
            var UTF8 = new UTF8Encoding();
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();
            var HashProvider = new MD5CryptoServiceProvider();

            try
            {
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(key));
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;
                byte[] dataToEncrypt = UTF8.GetBytes(text);
                var Encryptor = TDESAlgorithm.CreateEncryptor();
                byte[] result = Encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                retorno = Convert.ToBase64String(result);
            }
            catch
            {
                retorno = string.Empty;
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            return retorno;
        }

        public static string Descriptografar(string text, string key)
        {
            var retorno = string.Empty;
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();
            var HashProvider = new MD5CryptoServiceProvider();
            var UTF8 = new UTF8Encoding();

            try
            {

                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(key));
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;
                byte[] dataToDecrypt = Convert.FromBase64String(text);
                var Decryptor = TDESAlgorithm.CreateDecryptor();
                byte[] result = Decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);

                retorno = UTF8.GetString(result);
            }
            catch (Exception e)
            {
                return text;
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            return retorno;
        }

    }
}

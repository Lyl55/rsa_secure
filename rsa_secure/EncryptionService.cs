using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace rsa_secure
{
    public class EncryptionService
    {
        private static RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        public EncryptionService()
        {
            _privateKey = rsa.ExportParameters(true);
            _publicKey = rsa.ExportParameters(false);
        }
        public string GetPrivateKey()
        {
            var str = new StringWriter();
            var xml = new XmlSerializer(typeof(RSAParameters));
            xml.Serialize(str, _privateKey);
            return str.ToString();
        }
        public string GetPublicKey()
        {
            var str = new StringWriter();
            var xml = new XmlSerializer(typeof(RSAParameters));
            xml.Serialize(str, _publicKey);
            return str.ToString();
        }
        public string Encrypt(string enc)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(_publicKey);
            var bytes = Encoding.Unicode.GetBytes(enc);
            var data = rsa.Encrypt(bytes, false);
            return Convert.ToBase64String(data);
        }
        public string Decrypt(string dec)
        {
            var bytes = Convert.FromBase64String(dec);
            rsa.ImportParameters(_privateKey);
            var data = rsa.Decrypt(bytes, false);
            return Encoding.Unicode.GetString(data);
        }
    }
}

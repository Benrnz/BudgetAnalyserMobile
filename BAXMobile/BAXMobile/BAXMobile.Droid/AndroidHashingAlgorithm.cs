using System.Security.Cryptography;
using System.Text;
using BAXMobile.Service;

namespace BAXMobile.Droid
{
    public class AndroidHashingAlgorithm : IHashingAlgorithm
    {
        private static readonly SHA256 HashingFunction = SHA256.Create();

        public byte[] ComputeSha256Hash(byte[] data)
        {
            return HashingFunction.ComputeHash(data);
        }

        public byte[] ComputeHmacSha256Hash(string data, byte[] key)
        {
            var algorithm = "HmacSHA256";
            var kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = key;

            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }
    }
}
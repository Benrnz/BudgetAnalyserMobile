using BAXMobile.Service;
using System.Security.Cryptography;
using System.Text;

<<<<<<<< HEAD:BAXMobile3/BAXMobile3.App/BAXMobile3.App.Droid/AndroidHashingAlgorithm.cs
namespace BaxMobile3.Droid
========
namespace BaxMobile2.Droid
>>>>>>>> 3f69382e76fe6e3d62aeb5c45dd5de7d217ffded:BAXMobile/BaxMobile2.Droid/AndroidHashingAlgorithm.cs
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
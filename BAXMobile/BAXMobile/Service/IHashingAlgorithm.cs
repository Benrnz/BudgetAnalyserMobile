namespace BAXMobile.Service
{
    //Note: System.Security.Cryptography is not PCL compliant and cannot be used in any PCL.  But, it can be used from the Android project because a port of it is available in Mono.
    public interface IHashingAlgorithm
    {
        byte[] ComputeSha256Hash(byte[] data);
        byte[] ComputeHmacSha256Hash(string data, byte[] key);
    }
}

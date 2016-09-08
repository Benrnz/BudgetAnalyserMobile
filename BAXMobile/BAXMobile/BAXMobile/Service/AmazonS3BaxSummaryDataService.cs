using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BAXMobile.Model;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BAXMobile.Service
{
    public class AmazonS3BaxSummaryDataService : IBaxSummaryDataService
    {
        private readonly IHashingAlgorithm hashingAlgorithm;
        private readonly string accessKey;
        private readonly string secret;
        private const string TargetAwsRegion = "ap-southeast-2";
        private const string TargetAwsService = "s3";
        private const string Aws4HasingAlgorithm = "AWS4-HMAC-SHA256";
        private const string Aws4Request = "aws4_request";

        private const string Host = "baxmobilesummary.s3-ap-southeast-2.amazonaws.com";
        private static string resourcePath = "/MobileDataExport.json";

        public AmazonS3BaxSummaryDataService([NotNull] IHashingAlgorithm hashingAlgorithm, [NotNull] string accessKey, [NotNull] string secret)
        {
            if (hashingAlgorithm == null) throw new ArgumentNullException(nameof(hashingAlgorithm));
            if (accessKey == null) throw new ArgumentNullException(nameof(accessKey));
            if (secret == null) throw new ArgumentNullException(nameof(secret));
            this.hashingAlgorithm = hashingAlgorithm;
            this.accessKey = accessKey;
            this.secret = secret;
        }

        public async Task<SummarisedLedgerMobileData> DownloadDataAsync()
        {
            string jsonData;
            // following instructions from http://docs.aws.amazon.com/general/latest/gr/sigv4_signing.html
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://" + Host + resourcePath))
            {
                var dateTime = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");
                var date = DateTime.UtcNow.ToString("yyyyMMdd");

                client.BaseAddress = new Uri("https://" + Host);

                var requestPayloadHash = CalculateHash(string.Empty); // My request, as its a GET, will be empty.

                var canonicalisedResource = GetCanonicalisedResource(Host, dateTime, requestPayloadHash);
                var hashedCanonicalisedRequest = CalculateHash(canonicalisedResource);

                var stringToSign =
                    $"{Aws4HasingAlgorithm}\n"
                    + $"{dateTime}\n"
                    + $"{date}/{TargetAwsRegion}/{TargetAwsService}/{Aws4Request}\n"
                    + hashedCanonicalisedRequest;

                var signature = GetSignatureKey(this.secret, date, TargetAwsRegion, TargetAwsService, stringToSign);
                var authHeaderString =
                    $"{Aws4HasingAlgorithm} Credential={this.accessKey}/{date}/{TargetAwsRegion}/{TargetAwsService}/{Aws4Request}, SignedHeaders=host;x-amz-date, Signature={signature}";

                request.Headers.Host = Host;
                request.Headers.Add("X-Amz-Date", dateTime);
                request.Headers.Add("X-Amz-Content-Sha256", requestPayloadHash);
                request.Headers.TryAddWithoutValidation("Authorization", authHeaderString);
                // Note that Content-Type cannot be added to GET request using HttpClient - using Content-Type does seem to be common in examples found on the web including GET requests.

                var response = await client.SendAsync(request);
                jsonData = await response.Content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<SummarisedLedgerMobileData>(jsonData);
        }

        private string CalculateHash(string canonicalisedResource)
        {
            return ToHex(hashingAlgorithm.ComputeSha256Hash(Encoding.UTF8.GetBytes(canonicalisedResource)));
        }

        private string GetCanonicalisedResource(string host, string date, string requestPayloadHash)
        {
            /*
                CanonicalRequest =
                  HTTPRequestMethod + '\n' +
                  CanonicalURI + '\n' +
                  CanonicalQueryString + '\n' +
                  CanonicalHeaders + '\n' +
                  SignedHeaders + '\n' +
                  HexEncode(Hash(RequestPayload))                 */
            var canonicalisedResource = "GET\n" // Http Request Method 
                                        + $"{resourcePath}\n" // Url to file
                                        + "\n" // Query string - blank in this case
                                        + $"host:{host}\n"
                                        + $"x-amz-date:{date}\n\n"
                                        + "host;x-amz-date\n" // headers that will be included in the request and signature calc
                                        + requestPayloadHash; // Hashed payload of the request - in my case empty string
            Debug.WriteLine(canonicalisedResource);
            return canonicalisedResource;
        }

        private string ToHex(byte[] data)
        {
            string hex = BitConverter.ToString(data);
            return hex.Replace("-", "").ToLower();
        }

        private string ToBytes(byte[] data)
        {
            var builder = new StringBuilder();
            foreach (var b in data)
            {
                builder.Append($"{b} ");
            }

            return builder.ToString();
        }

        private byte[] HmacSha256(string data, byte[] key)
        {
            return this.hashingAlgorithm.ComputeHmacSha256Hash(data, key);
        }

        private string GetSignatureKey(string key, string dateStamp, string regionName, string serviceName, string stringToSign)
        {
            var secret = Encoding.UTF8.GetBytes(("AWS4" + key).ToCharArray());
            var date = HmacSha256(dateStamp, secret);
            var region = HmacSha256(regionName, date);
            var service = HmacSha256(serviceName, region);
            var derivedSigningKey = HmacSha256(Aws4Request, service);

            Debug.WriteLine("Derived-Signing-Key (hex) : " + ToHex(derivedSigningKey));
            Debug.WriteLine("Derived-Signing-Key (ints): " + ToBytes(derivedSigningKey));

            var signature = ToHex(HmacSha256(stringToSign, derivedSigningKey));
            Debug.WriteLine("Signature: " + signature);

            return signature;
        }
    }
}

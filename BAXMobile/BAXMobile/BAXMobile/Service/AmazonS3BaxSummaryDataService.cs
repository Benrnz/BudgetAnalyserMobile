using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using BAXMobile.Model;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BAXMobile.Service
{
    public class AmazonS3BaxSummaryDataService : IBaxSummaryDataService
    {
        private const string AwsBucketName = "baxmobilesummary";
        private const string AwsBucketFileName = "MobileDataExport.json";
        private readonly string accessKey;
        private readonly string secret;

        public AmazonS3BaxSummaryDataService([NotNull] string accessKey, [NotNull] string secret)
        {
            if (accessKey == null) throw new ArgumentNullException(nameof(accessKey));
            if (secret == null) throw new ArgumentNullException(nameof(secret));
            this.accessKey = accessKey;
            this.secret = secret;
        }

        public async Task<SummarisedLedgerMobileData> DownloadDataAsync()
        {
            GetObjectResponse response;
            using (var client = new AmazonS3Client(this.accessKey, this.secret, RegionEndpoint.APSoutheast2))
            {
                response = await client.GetObjectAsync(AwsBucketName, AwsBucketFileName);
            }

            string data;
            using (var reader = new StreamReader(response.ResponseStream))
            {
                data = await reader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<SummarisedLedgerMobileData>(data);
        }
    }
}

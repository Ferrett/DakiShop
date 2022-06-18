using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace DakiShop.Logic
{
    public static class AWSManager
    {
        public static string UserKey { get; set; }
        public static string SecretKey { get; set; }
        public static void UploadFile(System.IO.Stream localFilePath, string bucketName, string fileNameInS3, RegionEndpoint region)
        {
            IAmazonS3 client = new AmazonS3Client(UserKey,SecretKey, region);
            TransferUtility utility = new TransferUtility(client);
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            request.BucketName = bucketName; //no subdirectory just bucket name  
            
            request.Key = fileNameInS3; //file name up in S3  
            request.InputStream = localFilePath;
            utility.Upload(request); //commensing the transfer  

           
        }
    }
}


using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace SnowflakeDemo;

public class AmazonTools
{
    public AmazonS3Client GetS3Client(BasicAWSCredentials creds,
         RegionEndpoint endpointRegion = null)
    {
        var clientRegion = RegionEndpoint.USEast1;
        if (endpointRegion != null)
        {
            clientRegion = endpointRegion;
        }

        var client = 
             new AmazonS3Client(creds, clientRegion);
        return client;
    }

    public BasicAWSCredentials GetBasicAwsCredentials(string awsAccessKey, string awsSecret)
    {
        

        var retval = new BasicAWSCredentials(awsAccessKey, awsSecret);
        return retval;
    }

    public void UploadFile(AmazonS3Client client, 
        string bucketName, string fileName)
    {
        var ms = FileToMemoryStream(fileName);
        var utility = new TransferUtility(client);
        var req = new TransferUtilityUploadRequest();
        var fi = new FileInfo(fileName);
        utility.Upload(ms, bucketName, fi.Name);
    }

    public MemoryStream FileToMemoryStream(string fileName)
    {
        var fs = new FileStream(fileName, 
            FileMode.Open, FileAccess.Read);
        var ms = new MemoryStream();
        fs.CopyTo(ms);
        return ms;
    }
}
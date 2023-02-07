using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using CsvHelper;
using Snowflake.Data.Client;

namespace SnowflakeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (Environment.GetEnvironmentVariable("DEMO_KEY") == null ||
                Environment.GetEnvironmentVariable("DEMO_SECRET") == null||
                Environment.GetEnvironmentVariable("DEMO_SNOWFLAKE_USER") == null||
                Environment.GetEnvironmentVariable("DEMO_SNOWFLAKE_PASSWORD") == null)
            {
                Console.WriteLine("You need the following environmental " +
                                  "variables setup:\n" +
                                  "DEMO_KEY\n"+
                                  "DEMO_SECRET\n"+
                                  "DEMO_SNOWFLAKE_USER\n"+
                                  "DEMO_SNOWFLAKE_PASSWORD");
                Console.ReadKey();
                return;
            }
            var awsAccessKey = Environment.GetEnvironmentVariable("DEMO_KEY");
            var awsSecret = Environment.GetEnvironmentVariable("DEMO_SECRET");
            var snowflakeUser = Environment.GetEnvironmentVariable("DEMO_SNOWFLAKE_USER");
            var snowflakePassword = Environment.GetEnvironmentVariable("DEMO_SNOWFLAKE_PASSWORD");
            var snowflakeIdentifier = "SNOWFLAKEIDENTIFIERHERE";
            var databaseName = "CODE_MAGAZINE_DEMO";
            var tableName = "DEMO_DATA";

            var bucketName = "dashpoint-demo";
            var fileName = @"D:\data\junk\CodeSampleData.csv";
            var itemKey = "CodeSampleData.csv";

            var random = new RandomDataService();
            var csvTools = new CsvTools();
            var amazonTools = new AmazonTools();
            var snowflakeTools = new SnowflakeTools();

            
            var data = random.GetSamplePeople(250000);
            csvTools.WriteCsvFile(data,fileName,"|");
            amazonTools.UploadFile(amazonTools.GetS3Client(
                amazonTools.GetBasicAwsCredentials(awsAccessKey,awsSecret)),bucketName, fileName);

            var stage = snowflakeTools.GetStage(bucketName,itemKey,awsAccessKey, awsSecret);
            var format = snowflakeTools.GetFormat("|");
            var copyCommand = snowflakeTools.GetCopyCommand(databaseName,tableName, 
                stage, format);

            var connString = snowflakeTools.GetConnectionString(snowflakeIdentifier, 
                snowflakeUser, snowflakePassword,
                databaseName, tableName);
         
            //send the data
            using (var conn = new SnowflakeDbConnection()
                       { ConnectionString = connString })
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = copyCommand;
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Data Sent To Snowflake");

            Console.ReadKey();

        }
    }
}
namespace SnowflakeDemo;

public class SnowflakeTools
{
    public string GetStage(string bucketName, string itemKey,  string accessKey, string secretKey)
    {

        var retval = $"""

                 's3://{bucketName}/{itemKey}'   
            CREDENTIALS = (AWS_KEY_ID='{accessKey}'
            AWS_SECRET_KEY='{secretKey}') 
            FORCE=true 

            """;
        return retval;
    }


    public string GetFormat(string delimiter)
    {
        var retval = $"""

                 file_format=(type='csv' 
             COMPRESSION='AUTO' 
             FIELD_DELIMITER='{delimiter}' 
            FIELD_OPTIONALLY_ENCLOSED_BY = '{(char)34}'
             RECORD_DELIMITER = '\n' 
             SKIP_HEADER = 1 
             TRIM_SPACE = FALSE 
             ERROR_ON_COLUMN_COUNT_MISMATCH = TRUE 
             ESCAPE = 'NONE' 
             DATE_FORMAT = 'AUTO' 
             TIMESTAMP_FORMAT = 'AUTO' 
             NULL_IF = ('\\N','NULL') 
            )

            """;
        return retval;
    }

    public string GetCopyCommand(string databaseName, 
        string tableName, string stageInfo, string formatInfo)
    {
        var retval = $"""
            COPY INTO {databaseName}.PUBLIC.{tableName} FROM {stageInfo} {formatInfo};
          """;
        return retval;
    }

    public  string GetConnectionString(string snowflakeIdentifier, string userName, 
        string password,  string databaseName,
        string tableName)
    {
        var retval = $"""
            account={snowflakeIdentifier};user={userName};
            password={password};db={databaseName};schema=public;warehouse=COMPUTE_WH
          """;
        return retval;
    }
}
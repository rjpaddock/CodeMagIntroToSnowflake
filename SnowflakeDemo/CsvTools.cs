using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace SnowflakeDemo;

public class CsvTools
{
    public  void WriteCsvFile(dynamic dataToWrite, string outputFile, string delimiter)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture);

        //include header
        config.HasHeaderRecord = true;

        //change delimiter
        config.Delimiter = delimiter;

        //quote delimit
        config.ShouldQuote = args => true;

        using (var writer = new StreamWriter(outputFile))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecords(dataToWrite);
        }
    }
}

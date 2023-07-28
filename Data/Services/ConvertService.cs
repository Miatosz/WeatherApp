using WeatherApp.Data.Interfaces;

namespace WeatherApp.Data.Services;

public class ConvertService : IConvertService
{

    public byte[] ConvertDictionaryToTType(Dictionary<string, double> temperaturesData, Dictionary<string, double> averageTemperatureData, bool fahrenheit, string fileType)
    {
        IConverter converter;
        switch (fileType)
        {
            case "xlsx":
                converter = new XlsxConverter();
                break;
            case "csv":
                converter = new CsvConverter();
                break;
            case "txt":
                converter = new TxtConverter();
                break;
            default:
                throw new ArgumentException($"Unsupported converter type: {fileType}");
        }        

        return converter.Convert(temperaturesData, averageTemperatureData, fahrenheit);
    }
}

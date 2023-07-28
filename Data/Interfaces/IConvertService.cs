namespace WeatherApp.Data.Interfaces;

public interface IConvertService
{
    byte[] ConvertDictionaryToGivenType(Dictionary<string, double> temperaturesData, Dictionary<string, double> averageTemperatureData, bool fahrenheit, string fileType);
}

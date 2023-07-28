namespace WeatherApp.Data.Interfaces;

public interface IConverter
{
    static string _fahrehheitSign { get; } = "℉";
    static string _celciusSign { get; } = "℃";
    byte[] Convert(Dictionary<string, double> temperaturesData, Dictionary<string, double> averageTemperatureData, bool fahrenheit);
}

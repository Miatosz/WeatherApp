using System.Text;
using WeatherApp.Data.Interfaces;

namespace WeatherApp.Data;

public class CsvConverter : IConverter
{

    public byte[] Convert(Dictionary<string, double> temperatureData, Dictionary<string, double> averageTemperatureData, bool fahrenheit)
    {
        var degreeType = !fahrenheit ? IConverter._celciusSign : IConverter._fahrehheitSign;
        var sb = new StringBuilder();
        sb.AppendLine($"date,temperature {degreeType}");
        AppendDataIntoStringBuilder(temperatureData, sb);
        sb.AppendLine($"date,average temperature {degreeType}");
        AppendDataIntoStringBuilder(averageTemperatureData, sb);
        return System.Text.Encoding.UTF8.GetBytes(sb.ToString());
    }

    private void AppendDataIntoStringBuilder(Dictionary<string, double> weatherData, StringBuilder sb)
    {
        foreach (var kvp in weatherData)
        {
            sb.AppendLine($"{kvp.Key}\t{kvp.Value}");
        }
    }
}


using System.Text;
using WeatherApp.Data.Interfaces;

namespace WeatherApp.Data;

public class TxtConverter : IConverter
{
    public byte[] Convert(Dictionary<string, double> temperaturesData, Dictionary<string, double> averageTemperatureData, bool fahrenheit)
    {
        var degreeType = !fahrenheit ? IConverter._celciusSign : IConverter._fahrehheitSign;
        var sb = new StringBuilder();

        sb.AppendLine($"Date\tTemperature{degreeType}");
        AppendDataIntoStringBuilder(temperaturesData, sb);
        sb.AppendLine($"Date\taverageTemperature{degreeType}");
        AppendDataIntoStringBuilder(averageTemperatureData, sb);

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private void AppendDataIntoStringBuilder(Dictionary<string, double> weatherData, StringBuilder sb)
    {
        foreach (var kvp in weatherData)
        {
            sb.AppendLine($"{kvp.Key}\t{kvp.Value}");
        }
    }
}

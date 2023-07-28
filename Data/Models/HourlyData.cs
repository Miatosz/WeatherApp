using Newtonsoft.Json;

namespace WeatherApp.Data.Models;

public class HourlyData
{
    [JsonProperty("time")]
    public List<string> Time { get; set; }

    [JsonProperty("temperature_2m")]
    public List<double> Temperature2m { get; set; }
}

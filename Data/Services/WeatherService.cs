using Newtonsoft.Json;
using WeatherApp.Data.Interfaces;

namespace WeatherApp.Data.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private const string apiUrlHourlyPattern = "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&hourly=temperature_2m&timezone=auto";
    private const string apiUrlDailyPattern = "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&daily=temperature_2m_max&daily=temperature_2m_min&timezone=auto";
    private const string temperatureUnitFahrenheitParam = "&temperature_unit=fahrenheit";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Dictionary<string, double>> CalculateDailyAverage(string latitude, string longitude, bool fahrenheit)
    {
        var apiUrl = CreateDailyTemperaturesApiUrl(latitude, longitude, fahrenheit);
        
        try
        {
            var jsonData = DeserializeJsonData(apiUrl).Result;
            Dictionary<string, double> averageTemperatures = new Dictionary<string, double>();

            for (int i = 0; i < jsonData.daily.time.Count; i++)
            {
                string date = jsonData.daily.time[i];
                double maxTemperature = jsonData.daily.temperature_2m_max[i];
                double minTemperature = jsonData.daily.temperature_2m_min[i];
                double averageTemperature = (maxTemperature + minTemperature) / 2;

                averageTemperatures[date] = averageTemperature;
            }
            return averageTemperatures;

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }


    public async Task<Dictionary<string,double>> GetHourlyTemperatureData(string latitude, string longitude, bool fahrenheit)
    {
        var apiUrl = CreateHourlyTemperaturesApiUrl(latitude, longitude, fahrenheit);
        try
        {
            var jsonData = DeserializeJsonData(apiUrl).Result;
            Dictionary<string, double> hourlyTemperatures = new Dictionary<string, double>();

            for (int i = 0; i < jsonData.hourly.time.Count; i++)
            {
                string date = jsonData.hourly.time[i];
                double hourTemperature = jsonData.hourly.temperature_2m[i];

                hourlyTemperatures[date] = hourTemperature;
            }

            return hourlyTemperatures;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }            
    }


    private string CreateHourlyTemperaturesApiUrl(string latitude, string longitude, bool fahrenheit)
    {
        string apiUrl = String.Format(apiUrlHourlyPattern, latitude, longitude);
        return !fahrenheit ? apiUrl : apiUrl += temperatureUnitFahrenheitParam;
    }
    

    private string CreateDailyTemperaturesApiUrl(string latitude, string longitude, bool fahrenheit)
    {
        string apiUrl = String.Format(apiUrlDailyPattern, latitude, longitude);
        return !fahrenheit ? apiUrl : apiUrl += temperatureUnitFahrenheitParam;
    }


    private async Task<dynamic> DeserializeJsonData(string apiUrl)
    {
        var response = _httpClient.GetAsync(apiUrl).Result;
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<dynamic>(content);
    }
}

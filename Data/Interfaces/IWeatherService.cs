using System.Collections.Generic;
using WeatherApp.Data.Models;

namespace WeatherApp.Data.Interfaces;

public interface IWeatherService
{
    Task<Dictionary<string, double>> GetHourlyTemperatureData(string latitude, string longitude, bool fahrenheit);
    Task<Dictionary<string, double>> CalculateDailyAverage(string latitude, string longitude, bool fahrenheit);
}

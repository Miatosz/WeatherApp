using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Data.Models;

public class Coordinates
{
    [Required]
    [RegularExpression(@"^(-?(?:90(?:\.0+)?|[0-8]?[0-9](?:\.\d+)?))$", ErrorMessage = "Range -90 to 90")]
    public string Latitude { get; set; }
    [Required]
    [RegularExpression(@"^(-?(?:180(?:\.0+)?|(?:[0-9]?[0-9]|1[0-7][0-9])(?:\.\d+)?))$", ErrorMessage = "Range -180 to 180")]
    public string Longitude { get; set; }
}

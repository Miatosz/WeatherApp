using OfficeOpenXml;
using WeatherApp.Data.Interfaces;

namespace WeatherApp.Data;

public class XlsxConverter : IConverter
{
    public byte[] Convert(Dictionary<string, double> temperatureData, Dictionary<string, double> averageTemperatureData, bool fahrenheit)
    {
            var degreeType = !fahrenheit ? IConverter._celciusSign : IConverter._fahrehheitSign;
            using (var package = new ExcelPackage())
            {
                CreateExcelWorksheet(temperatureData, "Temperatures hourly", degreeType, false, package);
                CreateExcelWorksheet(averageTemperatureData, "Averages Temperatures", degreeType, true, package);

                using (MemoryStream stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    return stream.ToArray();
                }
            }            
        }
    private ExcelWorksheet CreateExcelWorksheet(Dictionary<string, double> temperatureData, string worksheetName, string degreeType, bool isAverage, ExcelPackage package)
    {
            var worksheet = package.Workbook.Worksheets.Add(worksheetName);
            int rowIndex = 2;
            worksheet.Cells[1, 1].Value = "Date";
            worksheet.Cells[1, 2].Value = !isAverage ? $"Temperature {degreeType}" : $"Average Temperature {degreeType}";
            foreach (var kvp in temperatureData)
            {
                worksheet.Cells[rowIndex, 1].Value = kvp.Key;
                worksheet.Cells[rowIndex, 2].Value = kvp.Value;
                rowIndex++;
            }
            worksheet.Cells.AutoFitColumns();

            return worksheet;        
    }
}

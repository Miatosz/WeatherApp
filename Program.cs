using MudBlazor.Services;
using WeatherApp.Data;
using WeatherApp.Data.Interfaces;
using WeatherApp.Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<XlsxConverter>();
builder.Services.AddScoped<CsvConverter>();
builder.Services.AddScoped<TxtConverter>();
builder.Services.AddScoped<IConvertService, ConvertService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

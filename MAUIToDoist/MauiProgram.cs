using MAUIToDoist.Interfaces;
using MAUIToDoist.Services;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net.Http;

namespace MAUIToDoist
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var logFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "app.log");

            // Настройка Serilog с ежедневным файлом
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog();

            builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri("https://localhost:5000/") });

            builder.Services.AddSingleton<BlazorViewService>();
            builder.Services.AddSingleton<ICView>(sp => sp.GetRequiredService<BlazorViewService>());

            // Scoped API сервис
            builder.Services.AddScoped<TodoApiService>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            #if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}

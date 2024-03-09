using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace HledacFirmy.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HledacFirmy.App.appsettings.json"))
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream ?? throw new ArgumentNullException(nameof(stream)))
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped(sp => new HttpClient { });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

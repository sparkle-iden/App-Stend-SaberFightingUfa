using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;


namespace MauiApp3
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder

                .UseMauiApp<App>()
                
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("helvetica_bold.otf", "helvetica_bold");
                    fonts.AddFont("helvetica_regular.otf", "helvetica_regular");
                    fonts.AddFont("arial.ttf", "arial");
                    fonts.AddFont("arialbd.ttf", "arialbd");

                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

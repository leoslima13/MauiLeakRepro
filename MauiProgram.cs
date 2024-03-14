using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Handlers.Items;

namespace MemoryLeakTest;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        CollectionViewHandler.Mapper.AppendToMapping("DisposableCollection", (handler, view) =>
        {
            view.Behaviors.Add(new DisposableBehavior());
        });

        return builder.Build();
    }
}
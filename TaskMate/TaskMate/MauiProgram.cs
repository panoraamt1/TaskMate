using Microsoft.Extensions.Logging;
using TaskMate.Data;
using Plugin.LocalNotification;


namespace TaskMate;

public static class MauiProgram
{
    // Allows us to access services globally if needed
    public static IServiceProvider Services { get; private set; }

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
        builder.UseLocalNotification();

        // Initialize SQLite for mobile platforms
        SQLitePCL.Batteries.Init();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // 1. Setup the Database Path
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "taskmate.db3");

        // 2. Register TaskDatabase as a Singleton 
        // (This means one instance is shared app-wide)
        builder.Services.AddSingleton<TaskDatabase>(s => new TaskDatabase(dbPath));

        var app = builder.Build();
        
        // Save the service provider for manual lookups
        Services = app.Services;

        return app;
    }
}
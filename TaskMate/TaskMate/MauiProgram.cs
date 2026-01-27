using Microsoft.Extensions.Logging;
using TaskMate.Data;
using Plugin.LocalNotification;


namespace TaskMate;

public static class MauiProgram
{
    public static IServiceProvider Services { get; private set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        builder.UseLocalNotification();


        string dbPath = Path.Combine(
            FileSystem.AppDataDirectory, "taskmate.db3");

        builder.Services.AddSingleton<TaskDatabase>(
            s => new TaskDatabase(dbPath));

        var app = builder.Build();
        Services = app.Services;

        return app;
    }
}
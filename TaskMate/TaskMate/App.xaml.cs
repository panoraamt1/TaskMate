namespace TaskMate;

using TaskMate.Views;
using System.Globalization;
using System.IO;

public partial class App : Application
{
    static Data.TaskDatabase database;
    public static Data.TaskDatabase Database
    {
        get
        {
            if (database == null)
                database = new Data.TaskDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TaskMate.db3"));
            return database;
        }
    }

    public App()
    {
        InitializeComponent();


        var culture = new CultureInfo("et-EE");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        MainPage = new NavigationPage(new HomePage());
    }
}
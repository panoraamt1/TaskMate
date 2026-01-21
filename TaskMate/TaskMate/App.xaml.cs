namespace TaskMate;

public partial class App : Application
{
    static Data.TaskDatabase database;
    public static Data.TaskDatabase Database
    {
        get
        {
            if (database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TaskMate.db3");
                database = new Data.TaskDatabase(dbPath);
            }
            return database;
        }
    }

    public App()
    {
        InitializeComponent();

        // Use AppShell to handle the List -> Detail navigation
        MainPage = new AppShell();
    }
}
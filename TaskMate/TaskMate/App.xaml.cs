using TaskMate.Data;
using TaskMate.Views;

namespace TaskMate
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // 1. Set a temporary blank page so the app doesn't crash on launch
            MainPage = new ContentPage { Content = new ActivityIndicator { IsRunning = true } };

            // 2. Start the database work
            StartApp();
        }

        private async void StartApp()
        {
            try
            {
                var database = Data.DatabaseService.Instance;
                var tasks = await database.GetTasksAsync();

                Models.TaskItem taskToDisplay;

                if (tasks != null && tasks.Count > 0)
                {
                    taskToDisplay = tasks[0];
                }
                else
                {
                    taskToDisplay = new Models.TaskItem
                    {
                        Name = "Database Test",
                        Priority = "Keskmine",
                        DueDate = DateTime.Now
                    };
                    await database.SaveTaskAsync(taskToDisplay);
                }

                // 3. Switch from the loading indicator to your actual page
                MainPage = new NavigationPage(new TaskDetailPage(taskToDisplay));
            }
            catch (Exception ex)
            {
                // If it still crashes, this will tell you why in the Output window
                System.Diagnostics.Debug.WriteLine($"Database Error: {ex.Message}");
            }
        }
    }
}
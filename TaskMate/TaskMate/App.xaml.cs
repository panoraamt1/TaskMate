using TaskMate.Views;

namespace TaskMate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(
                new TaskDetailPage(
                    new Models.TaskItem
                    {
                        IsDone = false,
                        Name = "Nupud lisada",
                        Description = "Edit, Delete",
                        DueDate = DateTime.Today.AddDays(7),
                        Priority = "Kõrge"
                    }
                )
            );
        }
    }
}
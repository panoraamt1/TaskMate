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
                        Name = "Test ülesanne",
                        Description = "See on detailvaate test",
                        DueDate = DateTime.Today.AddDays(3),
                        Priority = "Kõrge"
                    }
                )
            );
        }
    }
}
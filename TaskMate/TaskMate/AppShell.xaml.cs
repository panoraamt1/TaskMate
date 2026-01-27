using TaskMate.Views; 
namespace TaskMate;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registering routes allows GoToAsync("TaskDetailPage") to work
        Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
        Routing.RegisterRoute(nameof(AddTaskPage), typeof(AddTaskPage));
    }
}
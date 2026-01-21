namespace TaskMate;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register the detail page route
        Routing.RegisterRoute("TaskDetailPage", typeof(TaskMate.Views.TaskDetailPage));
    }
}
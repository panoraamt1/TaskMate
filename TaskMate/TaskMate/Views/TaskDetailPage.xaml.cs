namespace TaskMate.Views;

public partial class TaskDetailPage : ContentPage
{
    public TaskDetailPage()
    {
        InitializeComponent();
        // The ViewModel will automatically get the task via the QueryProperty
        BindingContext = new TaskMate.ViewModels.TaskDetailViewModel();
    }
}
namespace TaskMate.Views;

public partial class TaskDetailPage : ContentPage
{
    // Constructor now takes the TaskItem
    public TaskDetailPage(TaskMate.Models.TaskItem task)
    {
        InitializeComponent();
        // Pass the task to the ViewModel
        BindingContext = new TaskMate.ViewModels.TaskDetailViewModel(task);
    }
}
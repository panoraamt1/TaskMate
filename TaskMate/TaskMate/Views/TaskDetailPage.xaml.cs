using TaskMate.ViewModels;

namespace TaskMate.Views;

public partial class TaskDetailPage : ContentPage
{
    public TaskDetailPage()
    {
        InitializeComponent();
        // Use the parameterless constructor
        BindingContext = new TaskDetailViewModel();
    }
}
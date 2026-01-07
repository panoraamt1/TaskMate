using TaskMate.ViewModels;

namespace TaskMate.Views;

public partial class TasksListPage : ContentPage
{
    public TasksListPage()
    {
        InitializeComponent();
        BindingContext = new TasksListViewModel();
    }
}
using TaskMate.Models;
using TaskMate.ViewModels;

namespace TaskMate.Views;

public partial class TasksListPage : ContentPage
{
    public TasksListPage()
    {
        InitializeComponent();
        // The BindingContext is usually set here or via Dependency Injection
        BindingContext = new TasksListViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // This ensures the list refreshes every time you navigate back
        if (BindingContext is TasksListViewModel vm)
        {
            vm.LoadTasks();
        }
    }

    private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
    {
        var task = e.CurrentSelection.FirstOrDefault() as TaskItem;
        if (task == null) return;

        // Use Shell navigation to stay consistent with your ViewModel
        var navigationParameter = new Dictionary<string, object>
        {
            { "SelectedTask", task }
        };
        await Shell.Current.GoToAsync("TaskDetailPage", navigationParameter);

        // Clear selection so the row doesn't stay highlighted
        ((CollectionView)sender).SelectedItem = null;
    }

   

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }

    private async void OnAddNewTaskClicked(object sender, EventArgs e)
    {
        // Navigates to the add page
        await Shell.Current.GoToAsync(nameof(AddTaskPage));
    }
}
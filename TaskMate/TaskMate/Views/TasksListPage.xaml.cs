using System.Linq;
using TaskMate.Models;

namespace TaskMate.Views;

public partial class TasksListPage : ContentPage
{
    public TasksListPage()
    {
        InitializeComponent();
        // The ViewModel handles the database loading now
        BindingContext = new TaskMate.ViewModels.TasksListViewModel();
    }

    // This ensures the list refreshes every time you come back from the Detail Page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TaskMate.ViewModels.TasksListViewModel vm)
        {
            vm.LoadTasks();
        }
    }

    private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
    {
        var task = e.CurrentSelection.FirstOrDefault() as TaskItem;
        if (task == null) return;

        var navigationParameter = new Dictionary<string, object>
        {
            { "SelectedTask", task }
        };

        await Shell.Current.GoToAsync("TaskDetailPage", navigationParameter);

        // Deselect item
        ((CollectionView)sender).SelectedItem = null;
    }
}
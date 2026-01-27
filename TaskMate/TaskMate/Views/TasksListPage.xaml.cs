using System.Linq;
using TaskMate.Models;

namespace TaskMate.Views;

public partial class TasksListPage : ContentPage
{
    public TasksListPage()
    {
        InitializeComponent();
        BindingContext = new TaskMate.ViewModels.TasksListViewModel();
    }

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

        await Navigation.PushAsync(new TaskDetailPage(task));

        ((CollectionView)sender).SelectedItem = null;
    }

    private async void OnAddNewTaskClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTaskPage());
    }
}
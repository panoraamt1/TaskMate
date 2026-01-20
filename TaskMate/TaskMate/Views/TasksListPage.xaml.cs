using System.Linq;
using TaskMate.ViewModels;
using TaskMate.Models;

namespace TaskMate.Views;

public partial class TasksListPage : ContentPage
{
    public TasksListPage()
    {
        InitializeComponent();
        BindingContext = new TasksListViewModel(
            MauiProgram.Services.GetRequiredService<TaskDatabase>());

    }

    private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
    {
        var task = e.CurrentSelection.FirstOrDefault() as TaskItem;
        if (task == null)
            return;

        await Shell.Current.GoToAsync("TaskDetailPage");

        ((CollectionView)sender).SelectedItem = null;
    }
}

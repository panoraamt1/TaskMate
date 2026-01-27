using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskMate.Models;
using TaskMate.Data;

namespace TaskMate.ViewModels
{
    public class TasksListViewModel : BaseViewModel
    {
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand MarkDoneCommand { get; }

        public TasksListViewModel()
        {
            DeleteCommand = new Command<TaskItem>(DeleteTask);
            EditCommand = new Command<TaskItem>(EditTask);
            MarkDoneCommand = new Command<TaskItem>(MarkDone);

            LoadTasks();
        }

        public async void LoadTasks()
        {
            Tasks.Clear();
            // Using the lead's static database property
            var tasks = await App.Database.GetTasksAsync();

            foreach (var task in tasks)
                Tasks.Add(task);
        }

        private async void DeleteTask(TaskItem task)
        {
            if (task == null) return;
            await App.Database.DeleteTaskAsync(task);
            Tasks.Remove(task);
        }

        private async void MarkDone(TaskItem task)
        {
            if (task == null) return;
            task.IsDone = true;
            await App.Database.SaveTaskAsync(task);
            LoadTasks(); // Refresh list
        }

        private async void EditTask(TaskItem task)
        {
            if (task == null) return;

            var navigationParameter = new Dictionary<string, object>
    {
        { "SelectedTask", task }
    };

            // This now sends the user to your TaskDetailPage
            await Shell.Current.GoToAsync("TaskDetailPage", navigationParameter);
        }
    }
}
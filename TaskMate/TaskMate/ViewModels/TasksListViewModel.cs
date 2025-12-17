using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskMate.Models;
using TaskMate.Data;
using Microsoft.Maui.Controls;

namespace TaskMate.ViewModels
{
    public class TasksListViewModel : BaseViewModel
    {
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public TasksListViewModel()
        {
            DeleteCommand = new Command<TaskItem>(DeleteTask);
            EditCommand = new Command<TaskItem>(EditTask);

            LoadTasks();
        }

        private void LoadTasks()
        {
            var db = DatabaseService.Instance; // singleton teenus
            var allTasks = db.GetTasks(); // tagastab List<TaskItem>
            Tasks.Clear();
            foreach (var t in allTasks)
                Tasks.Add(t);
        }

        private void DeleteTask(TaskItem task)
        {
            var db = DatabaseService.Instance;
            db.DeleteTask(task.Id);
            Tasks.Remove(task);
        }

        private void EditTask(TaskItem task)
        {
            // Navigeeri EditTaskPage peale (hiljem lisame)
        }
    }
}

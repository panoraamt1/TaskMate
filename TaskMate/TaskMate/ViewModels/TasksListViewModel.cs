using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskMate.Models;
using TaskMate.Data;
using Microsoft.Maui.Controls;

namespace TaskMate.ViewModels
{
    public class TasksListViewModel : BaseViewModel
    {
        // 🔹 ObservableCollection, mis hoiab ülesandeid UI jaoks
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        // 🔹 Command’id nuppudele
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand MarkDoneCommand { get; }

        // 🔹 Andmebaas
        private readonly TaskDatabase _database;

        // 🔹 Konstruktor, mis saab DI kaudu TaskDatabase
        public TasksListViewModel(TaskDatabase database)
        {
            _database = database;

            DeleteCommand = new Command<TaskItem>(DeleteTask);
            EditCommand = new Command<TaskItem>(EditTask);
            MarkDoneCommand = new Command<TaskItem>(MarkDone);

            LoadTasks();
        }

        // 🔹 Laadib ülesanded andmebaasist
        private async void LoadTasks()
        {
            Tasks.Clear();
            var tasks = await _database.GetTasksAsync();

            foreach (var task in tasks)
                Tasks.Add(task);
        }

        // 🔹 Kustutab ülesande
        private async void DeleteTask(TaskItem task)
        {
            if (task == null) return;

            await _database.DeleteTaskAsync(task); // kustuta DB-st
            Tasks.Remove(task);                    // eemalda UI-st
        }

        // 🔹 Märgib ülesande tehtuks
        private async void MarkDone(TaskItem task)
        {
            if (task == null) return;

            task.IsDone = true;
            await _database.SaveTaskAsync(task);  // salvesta DB-sse
        }

        // 🔹 Navigeerib Edit/Detail vaatesse
        private async void EditTask(TaskItem task)
        {
            if (task == null) return;

            await Shell.Current.GoToAsync("TaskDetailPage"); // detaili leht on Hendriku teha
        }
    }
}

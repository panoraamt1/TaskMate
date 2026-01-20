using System.Windows.Input;
using TaskMate.Models;
using TaskMate.Data;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace TaskMate.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        private TaskItem task;

        public List<string> PriorityOptions { get; } = new List<string> { "Kõrge", "Keskmine", "Madal" };

        public TaskDetailViewModel(TaskItem task)
        {
            // Safety check: if task is null, create a blank one so the app doesn't crash
            this.task = task ?? new TaskItem { Name = "Uus", Priority = "Madal" };

            // Commands
            MarkDoneCommand = new Command(async () => await MarkDone());
            SaveCommand = new Command(async () => await SaveTask());
            DeleteCommand = new Command(async () => await DeleteTask());
        }

        public string Name
        {
            get => task.Name;
            set { task.Name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => task.Description;
            set { task.Description = value; OnPropertyChanged(); }
        }

        public DateTime DueDate
        {
            get => task.DueDate;
            set { task.DueDate = value; OnPropertyChanged(); }
        }

        public string Priority
        {
            get => task.Priority;
            set
            {
                task.Priority = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PriorityColor));
            }
        }

        public string PriorityColor => task.PriorityColor;
        public string StatusText => task.IsDone ? "Tehtud" : "Tegemata";
        public bool IsDone => task.IsDone;

        public ICommand MarkDoneCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private async Task MarkDone()
        {
            if (!task.IsDone)
            {
                task.IsDone = true;
                await DatabaseService.Instance.SaveTaskAsync(task);
                OnPropertyChanged(nameof(StatusText));
                OnPropertyChanged(nameof(IsDone));
            }
        }

        private async Task SaveTask()
        {
            await DatabaseService.Instance.SaveTaskAsync(task);

            // FIXED: Using Application.Current instead of Shell
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("Salvestatud", "Muudatused on salvestatud!", "OK");
            }
        }

        private async Task DeleteTask()
        {
            if (Application.Current?.MainPage == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Kustuta", "Kas soovid selle ülesande kustutada?", "Jah", "Ei");

            if (confirm)
            {
                await DatabaseService.Instance.DeleteTaskAsync(this.task);

                // FIXED: Using PopAsync to go back in a NavigationPage
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
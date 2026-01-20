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
            this.task = task ?? new TaskItem { Name = "Uus", Priority = "Madal" };

            // Initialize Commands
            MarkDoneCommand = new Command(async () => await ToggleDone());
            SaveCommand = new Command(async () => await SaveTask());
            DeleteCommand = new Command(async () => await DeleteTask());
        }

        // Editable Properties
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

        // UI Helper Properties
        public string PriorityColor => task.PriorityColor;
        public string StatusText => task.IsDone ? "Tehtud" : "Tegemata";
        public bool IsDone => task.IsDone;

        // Toggle Button Visuals
        public string MarkDoneButtonText => task.IsDone ? "Märgi tegemata" : "Märgi tehtuks";
        public string MarkDoneButtonColor => task.IsDone ? "#E67E22" : "#2ECC71";

        // Commands
        public ICommand MarkDoneCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private async Task ToggleDone()
        {
            task.IsDone = !task.IsDone;
            await DatabaseService.Instance.SaveTaskAsync(task);

            // Refresh all status-related UI
            OnPropertyChanged(nameof(StatusText));
            OnPropertyChanged(nameof(IsDone));
            OnPropertyChanged(nameof(MarkDoneButtonText));
            OnPropertyChanged(nameof(MarkDoneButtonColor));
        }

        private async Task SaveTask()
        {
            await DatabaseService.Instance.SaveTaskAsync(task);
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
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
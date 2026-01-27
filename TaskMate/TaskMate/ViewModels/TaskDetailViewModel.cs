using System.Windows.Input;
using TaskMate.Models;
using Microsoft.Maui.Controls;

namespace TaskMate.ViewModels
{
    // This attribute maps the dictionary key "SelectedTask" to the Task property below
    [QueryProperty(nameof(Task), "SelectedTask")]
    public class TaskDetailViewModel : BaseViewModel
    {
        private TaskItem _task;

        public List<string> PriorityOptions { get; } = new List<string> { "Kõrge", "Keskmine", "Madal" };

        public TaskItem Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged();
                RefreshProperties();
            }
        }

        public TaskDetailViewModel()
        {
            // Set default values in case we are adding a NEW task
            if (Task == null)
                Task = new TaskItem { DueDate = DateTime.Now, Priority = "Madal" };

            MarkDoneCommand = new Command(async () => await ToggleDone());
            SaveCommand = new Command(async () => await SaveTask());
            DeleteCommand = new Command(async () => await DeleteTask());
        }

        // UI Bindings
        public string Name
        {
            get => Task?.Name;
            set { if (Task != null) { Task.Name = value; OnPropertyChanged(); } }
        }

        public string Description
        {
            get => Task?.Description;
            set { if (Task != null) { Task.Description = value; OnPropertyChanged(); } }
        }

        public DateTime DueDate
        {
            get => Task?.DueDate ?? DateTime.Now;
            set { if (Task != null) { Task.DueDate = value; OnPropertyChanged(); } }
        }

        public string Priority
        {
            get => Task?.Priority;
            set
            {
                if (Task != null)
                {
                    Task.Priority = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PriorityColor));
                }
            }
        }

        public string PriorityColor => Task?.PriorityColor ?? "Gray";
        public string StatusText => Task?.IsDone == true ? "Tehtud" : "Tegemata";
        public string MarkDoneButtonText => Task?.IsDone == true ? "Märgi tegemata" : "Märgi tehtuks";
        public string MarkDoneButtonColor => Task?.IsDone == true ? "#E67E22" : "#2ECC71";

        public ICommand MarkDoneCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(DueDate));
            OnPropertyChanged(nameof(Priority));
            OnPropertyChanged(nameof(StatusText));
            OnPropertyChanged(nameof(MarkDoneButtonText));
            OnPropertyChanged(nameof(MarkDoneButtonColor));
            OnPropertyChanged(nameof(PriorityColor));
        }

        private async Task ToggleDone()
        {
            if (Task == null) return;
            Task.IsDone = !Task.IsDone;
            await App.Database.SaveTaskAsync(Task);
            RefreshProperties();
        }

        private async Task SaveTask()
        {
            if (Task == null) return;
            await App.Database.SaveTaskAsync(Task);
            await Shell.Current.DisplayAlert("Salvestatud", "Muudatused on salvestatud!", "OK");
            await Shell.Current.GoToAsync(".."); // Go back to the list
        }

        private async Task DeleteTask()
        {
            if (Task == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Kustuta", "Kas soovid ülesande kustutada?", "Jah", "Ei");
            if (confirm)
            {
                await App.Database.DeleteTaskAsync(Task);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
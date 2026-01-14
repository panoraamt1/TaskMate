using System;
using System.Windows.Input;
using TaskMate.Models;
using Microsoft.Maui.Controls;

namespace TaskMate.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        private TaskItem _task;

        public TaskItem Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DueDate));
                OnPropertyChanged(nameof(Priority));
                OnPropertyChanged(nameof(IsDone));
                OnPropertyChanged(nameof(PriorityColor));
                OnPropertyChanged(nameof(Description));
            }
        }

        // Exposed properties for binding (same style as list)
        public string Name => Task?.Name;
        public DateTime DueDate => Task?.DueDate ?? DateTime.Today;
        public string Priority => Task?.Priority;
        public bool IsDone => Task?.IsDone ?? false;
        public string Description => Task?.Description;

        public Color PriorityColor =>
            Priority switch
            {
                "Kõrge" => Colors.Red,
                "Keskmine" => Colors.Orange,
                "Madal" => Colors.Green,
                _ => Colors.Black
            };

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand MarkDoneCommand { get; }

        public TaskDetailViewModel(TaskItem task)
        {
            Task = task;

            EditCommand = new Command(EditTask);
            DeleteCommand = new Command(DeleteTask);
            MarkDoneCommand = new Command(MarkDone);
        }

        private void EditTask()
        {
            // Hiljem: navigeeri EditTaskPage peale
        }

        private void DeleteTask()
        {
            // Hiljem: kustuta andmebaasist või mine tagasi listi
        }

        private void MarkDone()
        {
            Task.IsDone = true;
            OnPropertyChanged(nameof(IsDone));
        }
    }
}
using System.Windows.Input;
using TaskMate.Models;
using Microsoft.Maui.Controls;

namespace TaskMate.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        private TaskItem task;

        public TaskDetailViewModel(TaskItem task)
        {
            this.task = task;

            // Commands
            MarkDoneCommand = new Command(MarkDone);
            EditCommand = new Command(EditTask);
            DeleteCommand = new Command(DeleteTask);
        }

        // Expose properties for binding (pass-through to task)
        public string Name => task.Name;
        public string Description => task.Description;
        public DateTime DueDate => task.DueDate;
        public string Priority => task.Priority;
        public string PriorityColor => task.PriorityColor;

        // New computed property for status text
        public string StatusText => task.IsDone ? "Tehtud" : "Tegemata";

        // Commands
        public ICommand MarkDoneCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private void MarkDone()
        {
            if (!task.IsDone)
            {
                task.IsDone = true;
                OnPropertyChanged(nameof(StatusText));
                // Also notify if you bind IsDone directly somewhere:
                OnPropertyChanged(nameof(IsDone));
            }
        }

        private void EditTask()
        {
            // TODO: Implement edit logic
        }

        private void DeleteTask()
        {
            // TODO: Implement delete logic
        }

        // Optional: expose IsDone if needed
        public bool IsDone => task.IsDone;
    }
}
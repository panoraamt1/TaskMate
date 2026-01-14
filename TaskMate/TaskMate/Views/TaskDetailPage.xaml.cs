using TaskMate.Models;
using TaskMate.ViewModels;

namespace TaskMate.Views
{
    public partial class TaskDetailPage : ContentPage
    {
        public TaskDetailPage(TaskItem task)
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel(task);
        }
    }
}

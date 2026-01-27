using TaskMate.Views;
using TaskMate.Models;

namespace TaskMate.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTaskPage());
        }

        private async void OnTasksClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TasksListPage());
        }

        private async void OnTaskDetailClicked(object sender, EventArgs e)
        {
            TaskItem selectedTask = new TaskItem(); // or get the real one
            await Navigation.PushAsync(new TaskDetailPage(selectedTask));
        }
    }
}

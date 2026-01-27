using TaskMate.Views;

namespace TaskMate.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnTasksClicked(object sender, EventArgs e)
        {
            // The "///" or "//" tells Shell: 
            // "Don't push this on top of Home, switch the view to the TasksListPage branch."
            await Shell.Current.GoToAsync("//TasksListPage");
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            // AddTaskPage is a "RegisterRoute" (sub-page), so we keep it as-is
            await Shell.Current.GoToAsync(nameof(AddTaskPage));
        }
    }
}
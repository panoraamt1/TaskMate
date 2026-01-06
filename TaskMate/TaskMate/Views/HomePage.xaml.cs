namespace TaskMate.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void AddTask_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTaskPage());
    }

    private async void TasksList_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TasksListPage());
    }

    private async void TodayTasks_Clicked(object sender, EventArgs e)
    {
        // Võid filterdatud TasksListPage'i kasutada
        await Navigation.PushAsync(new TasksListPage(showTodayOnly: true));
    }
}
using TaskMate.Models;
using TaskMate.Data;

namespace TaskMate.Views
{
    public partial class AddTaskPage : ContentPage
    {
        private readonly TaskDatabase _database;

        public AddTaskPage()
        {
            InitializeComponent();
            _database = MauiProgram.Services.GetRequiredService<TaskDatabase>();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var task = new TaskItem
            {
                Name = nameEntry.Text,
                Description = descriptionEditor.Text,
                DueDate = dueDatePicker.Date,
                ReminderTime = reminderTimePicker.Time,
                Priority = priorityPicker.SelectedItem?.ToString()
            };

            await _database.SaveTaskAsync(task);
            await DisplayAlert("Salvestatud", "Ülesanne lisatud!", "OK");
            await Navigation.PopAsync();
        }
    }
}

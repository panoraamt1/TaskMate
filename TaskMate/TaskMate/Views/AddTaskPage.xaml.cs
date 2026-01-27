using TaskMate.Models;
using TaskMate.Data;
using Plugin.LocalNotification;
using Microsoft.Maui.Devices;


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

            if (task.ReminderTime.HasValue &&
            (DeviceInfo.Platform == DevicePlatform.Android ||
            DeviceInfo.Platform == DevicePlatform.iOS))
            {
                var notifyTime = task.DueDate.Date + task.ReminderTime.Value;

                var notification = new NotificationRequest
                {
                    NotificationId = new Random().Next(1000, 9999),
                    Title = "Meeldetuletus",
                    Description = task.Name,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = notifyTime
                    }
                };

                await LocalNotificationCenter.Current.Show(notification);
            }

            await DisplayAlert("Salvestatud", "Ülesanne lisatud!", "OK");
            await Navigation.PopAsync();
        }
    }
}

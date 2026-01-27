using TaskMate.Models;
using Plugin.LocalNotification;

namespace TaskMate.Views
{
    public partial class AddTaskPage : ContentPage
    {
        public AddTaskPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // 1. Create the task object from UI inputs
            var task = new TaskItem
            {
                Name = nameEntry.Text,
                Description = descriptionEditor.Text,
                DueDate = dueDatePicker.Date,
                ReminderTime = reminderTimePicker.Time,
                Priority = priorityPicker.SelectedItem?.ToString() ?? "Madal"
            };

            // 2. Save to database using the static App property
            await App.Database.SaveTaskAsync(task);

            // 3. Handle Notifications (Logic stays the same)
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

            // 4. Alert and Navigate back
            await DisplayAlert("Salvestatud", "Ülesanne lisatud!", "OK");

            // ".." means "go back one page" in Shell
            await Shell.Current.GoToAsync("..");
        }
    }
}
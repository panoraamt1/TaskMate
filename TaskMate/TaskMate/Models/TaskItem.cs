using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskMate.Models
{
    // Keeping public so all pages can access it
    public class TaskItem : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string name;
        public string Name 
        { 
            get => name; 
            set { name = value; OnPropertyChanged(); } 
        }

        private string description;
        public string Description 
        { 
            get => description; 
            set { description = value; OnPropertyChanged(); } 
        }

        private DateTime dueDate;
        public DateTime DueDate 
        { 
            get => dueDate; 
            set { dueDate = value; OnPropertyChanged(); } 
        }

        private TimeSpan? reminderTime;
        public TimeSpan? ReminderTime
        {
            get => reminderTime;
            set { reminderTime = value; OnPropertyChanged(); }
        }

        private string priority;
        public string Priority 
        { 
            get => priority; 
            set 
            { 
                priority = value; 
                OnPropertyChanged(); 
                // This triggers the UI to re-check the color whenever the priority text changes
                OnPropertyChanged(nameof(PriorityColor)); 
            } 
        }

        private bool isDone = false;
        public bool IsDone 
        { 
            get => isDone; 
            set { isDone = value; OnPropertyChanged(); } 
        }

        // Helper property for UI coloring
        [Ignore] // Tells SQLite not to try and save this color string to the database
        public string PriorityColor
        {
            get
            {
                return Priority switch
                {
                    "Kõrge" => "Red",
                    "Keskmine" => "Orange",
                    "Madal" => "Green",
                    _ => "Gray"
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
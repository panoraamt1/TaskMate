using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskMate.Models
{
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

        private string priority;
        public string Priority 
        { 
            get => priority; 
            set { priority = value; OnPropertyChanged(); OnPropertyChanged(nameof(PriorityColor)); } 
        }

        private bool isDone = false;
        public bool IsDone
        {
            get => isDone;
            set
            {
                isDone = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotDone));
            }
        }

        // UI jaoks – kas ülesanne pole veel tehtud
        public bool IsNotDone => !IsDone;


        // UI jaoks arvutatav omadus
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

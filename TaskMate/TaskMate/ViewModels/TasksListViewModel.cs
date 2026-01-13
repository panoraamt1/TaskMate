using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskMate.Models;
using TaskMate.Data;
using Microsoft.Maui.Controls;

namespace TaskMate.ViewModels
{
    public class TasksListViewModel : BaseViewModel
    {
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand MarkDoneCommand { get; }  // <- siia

        public TasksListViewModel()
        {
            DeleteCommand = new Command<TaskItem>(DeleteTask);
            EditCommand = new Command<TaskItem>(EditTask);
            MarkDoneCommand = new Command<TaskItem>(MarkDone);  // <- ja siia konstruktoris


            LoadTasks();
        }

        private void LoadTasks()
        {
            // FIKTIIVANDMED – ainult vaate testimiseks
            var allTasks = new List<TaskItem>
            {
                new TaskItem
                {
                    Name = "Õpi MAUI",
                    DueDate = DateTime.Today,
                    Priority = "Kõrge"
                },
                new TaskItem
                {
                    Name = "Kirjuta projektitöö",
                    DueDate = DateTime.Today.AddDays(2),
                    Priority = "Keskmine"
                }
            };

            Tasks.Clear();
            foreach (var t in allTasks)
                Tasks.Add(t);
        }

        private void DeleteTask(TaskItem task)
        {
            // ainult vaate testimiseks – eemalda Tasks kogumist
            Tasks.Remove(task);
        }

        private void EditTask(TaskItem task)
        {
            // Navigeeri EditTaskPage peale (hiljem lisame)
        }

        private void MarkDone(TaskItem task)
        {
            task.IsDone = true;
            OnPropertyChanged(nameof(Tasks));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TaskMate.Models
{
    class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TimeSpan? ReminderTime { get; set; }
        public string Priority { get; set; }
    }
}

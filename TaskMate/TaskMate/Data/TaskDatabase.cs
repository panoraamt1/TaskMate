using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TaskMate.Models;


namespace TaskMate.Data
{
    class TaskDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public TaskDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskItem>().Wait();
        }

        public Task<List<TaskItem>> GetTasksAsync()
            => _database.Table<TaskItem>().ToListAsync();

        public Task<int> SaveTaskAsync(TaskItem task)
            => _database.InsertAsync(task);

    }
}

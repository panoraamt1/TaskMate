using SQLite;
using TaskMate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace TaskMate.Data
{
    public class DatabaseService
    {
        private static DatabaseService _instance;
        public static DatabaseService Instance => _instance ??= new DatabaseService();

        private readonly SQLiteAsyncConnection _database;

        private DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "taskmate.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // REMOVED the .Wait() line from here!
        }

        // We add a helper method to ensure the table exists before we use it
        private async Task Init()
        {
            await _database.CreateTableAsync<TaskItem>();
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            await Init(); // Ensure table is ready
            return await _database.Table<TaskItem>().ToListAsync();
        }

        public async Task<int> SaveTaskAsync(TaskItem task)
        {
            await Init(); // Ensure table is ready
            if (task.Id != 0)
                return await _database.UpdateAsync(task);
            else
                return await _database.InsertAsync(task);
        }

        public async Task<int> DeleteTaskAsync(TaskItem task)
        {
            await Init(); // Ensure table is ready
            return await _database.DeleteAsync(task);
        }
    }
}
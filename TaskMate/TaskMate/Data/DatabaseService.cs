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

           
            _database.CreateTableAsync<TaskItem>().Wait();
        }

        
        public Task<List<TaskItem>> GetTasksAsync()
            => _database.Table<TaskItem>().ToListAsync();

       
        public Task<int> SaveTaskAsync(TaskItem task)
        {
            if (task.Id != 0)
                return _database.UpdateAsync(task); 
            else
                return _database.InsertAsync(task); 
        }

        
        public Task<int> DeleteTaskAsync(TaskItem task)
            => _database.DeleteAsync(task);
    }
}
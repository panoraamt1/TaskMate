using SQLite;
using TaskMate.Models;

namespace TaskMate.Data
{
    public class DatabaseService
    {
        private static DatabaseService _instance;
        public static DatabaseService Instance => _instance ??= new DatabaseService();

        private SQLiteConnection _db;

        private DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "taskmate.db3");
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<TaskItem>();
        }

        public List<TaskItem> GetTasks() => _db.Table<TaskItem>().ToList();

        public void AddTask(TaskItem task) => _db.Insert(task);

        public void DeleteTask(int id) => _db.Delete<TaskItem>(id);

        public void UpdateTask(TaskItem task) => _db.Update(task);
    }
}

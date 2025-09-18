using ToDoList.Core;

namespace ToDoList.Infrastructure.Database;

public interface IToDoTasksDao
{
    Task<IEnumerable<ToDoTask>> GetValidUserTasks(string username, DateTime currentDate);
    Task<IEnumerable<ToDoTask>> GetExpiredUserTasks(string username, DateTime currentDate);
    Task<IEnumerable<ToDoTask>> GetCompletedUserTasks(string username);
    Task CreateTaskAsync(ToDoTask task);
    Task UpdateTaskAsync(ToDoTask task);
    Task RemoveTaskAsync(Guid taskId);
}
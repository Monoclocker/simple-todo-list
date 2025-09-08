using ToDoList.Core;

namespace ToDoList.Infrastructure.Database;

public interface IToDoTasksDao
{
    Task<IEnumerable<ToDoTask>> GetAllByUserAsync(Guid userId);
    Task<ToDoTask?> GetByIdAsync(Guid id);
    Task CreateTaskAsync(ToDoTask task);
    Task UpdateTaskAsync(ToDoTask task);
    Task RemoveTaskAsync(Guid taskId);
}
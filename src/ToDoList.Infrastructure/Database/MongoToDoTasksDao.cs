using MongoDB.Driver;
using ToDoList.Core;

namespace ToDoList.Infrastructure.Database;

internal sealed class MongoToDoTasksDao(IMongoClient client) : IToDoTasksDao
{
    private readonly IMongoCollection<ToDoTask> _collection = client
        .GetDatabase("ToDoList")
        .GetCollection<ToDoTask>(nameof(ToDoTask));
    
    public async Task<IEnumerable<ToDoTask>> GetAllByUserAsync(Guid userId)
    { 
        List<ToDoTask> tasks = await _collection
            .Find(x => x.UserId == userId)
            .ToListAsync();
        
        return tasks;
    }

    public async Task<ToDoTask?> GetByIdAsync(Guid id)
    {
        ToDoTask? element = await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();

        return element;
    }

    public Task CreateTaskAsync(ToDoTask task)
    {
        return _collection.InsertOneAsync(task);
    }
    
    public Task UpdateTaskAsync(ToDoTask task)
    {
        FilterDefinition<ToDoTask> filter = Builders<ToDoTask>
            .Filter
            .Eq(x => x.Id, task.Id);

        UpdateDefinition<ToDoTask> updateDefinition = Builders<ToDoTask>
            .Update
            .Set(x => x.IsCompleted, task.IsCompleted)
            .Set(x => x.ValidUntil, task.ValidUntil)
            .Set(x => x.Task, task.Task)
            .Set(x => x.Description, task.Description);

        return _collection.UpdateOneAsync(filter, updateDefinition);
    }

    public Task RemoveTaskAsync(Guid taskId)
    {
        FilterDefinition<ToDoTask> filter = Builders<ToDoTask>
            .Filter
            .Eq(x => x.Id, taskId);

        return _collection.DeleteOneAsync(filter);
    }
}
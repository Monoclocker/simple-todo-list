using MongoDB.Driver;
using ToDoList.Core;

namespace ToDoList.Infrastructure.Database;

internal sealed class MongoToDoTasksDao(IMongoClient client) : IToDoTasksDao
{
    private readonly IMongoCollection<ToDoTask> _collection = client
        .GetDatabase("ToDoList")
        .GetCollection<ToDoTask>(nameof(ToDoTask));

    public async Task<IEnumerable<ToDoTask>> GetValidUserTasks(string username, DateTime currentDate)
    {
        FilterDefinition<ToDoTask> usernameEqualityFilter = Builders<ToDoTask>
            .Filter
            .Eq(x => x.Username, username);
        
        FilterDefinition<ToDoTask> dateFilter = Builders<ToDoTask>
            .Filter
            .Gte(x => x.ValidUntil, currentDate);
        
        var queryFilter = dateFilter & usernameEqualityFilter;

        var tasks = await _collection.FindAsync(queryFilter);

        return await tasks.ToListAsync();
    }

    public async Task<IEnumerable<ToDoTask>> GetExpiredUserTasks(string username, DateTime currentDate)
    {
        FilterDefinition<ToDoTask> usernameEqualityFilter = Builders<ToDoTask>
            .Filter
            .Eq(x => x.Username, username);
        
        FilterDefinition<ToDoTask> dateFilter = Builders<ToDoTask>
            .Filter
            .Lte(x => x.ValidUntil, currentDate);
        
        var queryFilter = dateFilter & usernameEqualityFilter;

        var tasks = await _collection.FindAsync(queryFilter);

        return await tasks.ToListAsync();
    }

    public async Task<IEnumerable<ToDoTask>> GetCompletedUserTasks(string username)
    {
        FilterDefinition<ToDoTask> usernameEqualityFilter = Builders<ToDoTask>
            .Filter
            .Eq(x => x.Username, username);

        FilterDefinition<ToDoTask> completionFilter = Builders<ToDoTask>
            .Filter
            .Eq(x => x.IsCompleted, true);
        
        var queryFilter = completionFilter & usernameEqualityFilter;

        var tasks = await _collection.FindAsync(queryFilter);

        return await tasks.ToListAsync();
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
            .Set(x => x.Title, task.Title)
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
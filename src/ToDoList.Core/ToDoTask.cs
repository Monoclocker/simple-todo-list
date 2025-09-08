namespace ToDoList.Core;

public sealed class ToDoTask
{
    public Guid Id { get; private set; }
    
    public string Task { get; private set; }
    
    public string Description { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public DateTime CreationDate { get; }
    
    public DateTime ValidUntil { get; private set; }
    
    public bool IsCompleted { get; private set; }

    private ToDoTask(
        string task,
        string description,
        DateTime creationDate,
        DateTime validUntil,
        Guid userId)
    {
        Id = Guid.CreateVersion7();
        UserId = userId;
        Task = task;
        Description = description;
        CreationDate = creationDate;
        ValidUntil = validUntil;
    }

    public ToDoTask(
        Guid id,
        string task,
        string description,
        DateTime creationDate,
        DateTime validUntil,
        Guid userId,
        bool isCompleted)
    {
        Id = id;
        Task = task;
        Description = description;
        CreationDate = creationDate;
        ValidUntil = validUntil;
        UserId = userId;
        IsCompleted = isCompleted;
    }

    public ToDoTaskResult UpdateValidTime(DateTime newValidUntil)
    {
        if (newValidUntil < CreationDate) 
            return ToDoTaskResult.TaskCantBeValidBeforeCreationTime();
        
        ValidUntil = newValidUntil;
        
        return ToDoTaskResult.Success();
    }

    public ToDoTaskResult MakeCompleted()
    {
        if (IsCompleted) return ToDoTaskResult.TaskAlreadyCompleted();
        
        IsCompleted = true;
        
        return ToDoTaskResult.Success();
    }

    public ToDoTaskResult MakeIncompleted()
    {
        if (!IsCompleted) return ToDoTaskResult.TaskAlreadyIncompleted();
        
        IsCompleted = false;
        
        return ToDoTaskResult.Success();
    }
    
    public void UpdateTask(string newTask)
    {
        Task = newTask;
    }

    public void UpdateDescription(string newDescription)
    {
        Task = newDescription;
    }

    public static ToDoTaskResult CreateNew(
        string task,
        string description,
        DateTime creationDate,
        DateTime validUntil,
        Guid userId,
        out ToDoTask? createdTask)
    {
        createdTask = null;
        
        if (creationDate > validUntil)
            return ToDoTaskResult.TaskCantBeValidBeforeCreationTime();

        createdTask = new ToDoTask(
            task,
            description,
            creationDate,
            validUntil,
            userId);
        
        return ToDoTaskResult.Success();
    }
}
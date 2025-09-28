namespace ToDoList.Core;

public sealed class ToDoTask
{
    private static readonly TimeSpan TimeBeforeExpirationToStartNotification 
        = TimeSpan.FromDays(7);
    
    public Guid Id { get; private set; }
    
    public string Title { get; private set; }
    
    public string Description { get; private set; }
    
    public string Username { get; private set; }
    
    public DateTime CreationDate { get; }
    
    public DateTime? ValidUntil { get; private set; }
    
    public bool IsCompleted { get; private set; }
    
    
    
    private ToDoTask(
        string title,
        string description,
        DateTime creationDate,
        DateTime validUntil,
        string username)
    {
        Id = Guid.CreateVersion7();
        Username = username;
        Title = title;
        Description = description;
        CreationDate = creationDate;
        ValidUntil = validUntil;
    }

    public ToDoTask(
        Guid id,
        string title,
        string description,
        DateTime creationDate,
        DateTime? validUntil,
        string username,
        bool isCompleted)
    {
        Id = id;
        Title = title;
        Description = description;
        CreationDate = creationDate;
        ValidUntil = validUntil;
        Username = username;
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
        Title = newTask;
    }

    public void UpdateDescription(string newDescription)
    {
        Title = newDescription;
    }

    public bool IsAboutToExpire(DateTime currentDateTime)
    {
        return ValidUntil.HasValue 
               && currentDateTime.Subtract(ValidUntil.Value) 
                <= TimeBeforeExpirationToStartNotification;
    }

    public static ToDoTaskResult CreateNew(
        string task,
        string description,
        DateTime creationDate,
        DateTime validUntil,
        string username,
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
            username);
        
        return ToDoTaskResult.Success();
    }
}
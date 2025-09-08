namespace ToDoList.Core;

public sealed class ToDoTaskResult
{
    public string? ErrorMessage { get; }

    public bool IsSuccess => ErrorMessage is null;

    public override bool Equals(object? obj)
    {
        return obj is ToDoTaskResult result && result.ErrorMessage == ErrorMessage;
    }

    public override int GetHashCode()
    {
        return (ErrorMessage != null ? ErrorMessage.GetHashCode() : 0);
    }

    private ToDoTaskResult(string message) => ErrorMessage = message;
    
    private ToDoTaskResult() => ErrorMessage = null;

    public static ToDoTaskResult TaskCantBeValidBeforeCreationTime()
        => new("Дата, до которой действительна задача не может быть раньше создания задачи");

    public static ToDoTaskResult TaskAlreadyCompleted()
        => new("Задача уже отмечена как выполненная");

    public static ToDoTaskResult TaskAlreadyIncompleted()
        => new("Задача уже отмечена как невыполненная");

    public static ToDoTaskResult Success() => new();
}
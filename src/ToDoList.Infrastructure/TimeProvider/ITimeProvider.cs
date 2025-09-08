namespace ToDoList.Infrastructure.TimeProvider;

public interface ITimeProvider
{
    public DateTime Now { get; }
}
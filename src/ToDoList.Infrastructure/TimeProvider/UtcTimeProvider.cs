namespace ToDoList.Infrastructure.TimeProvider;

internal sealed class UtcTimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}
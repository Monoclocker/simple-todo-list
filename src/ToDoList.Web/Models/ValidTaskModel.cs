namespace ToDoList.Web.Models;

public sealed record ValidTaskModel(
    Guid Id,
    string Title,
    string? Description,
    DateTime CreationTime,
    DateTime? ValidUntil,
    bool IsAboutToExpire);
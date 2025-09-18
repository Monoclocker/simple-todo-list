namespace ToDoList.Web.Models;

public sealed record TaskModel(Guid Id, string Title, string Description, DateTime? ExpirationDate);
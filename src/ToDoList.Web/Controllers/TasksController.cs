using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core;
using ToDoList.Infrastructure.Database;
using ToDoList.Infrastructure.TimeProvider;
using ToDoList.Web.Models;

namespace ToDoList.Web.Controllers;

public sealed class TasksController(IToDoTasksDao tasksDao, ITimeProvider timeProvider) : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Valid()
    {
        return View();
    }

    private TaskModel Map(ToDoTask task)
    {
        return new TaskModel(
            task.Id,
            task.Title,
            task.Description,
            task.ValidUntil);
    }
}
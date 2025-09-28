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
        DateTime currentTime = timeProvider.Now;
        
        string username = User.Identity?.Name ?? throw new ArgumentException("Username is required.");
        
        var tasks = await tasksDao.GetValidUserTasks(username, currentTime);
        
        return PartialView("_ValidTasks", tasks.Select(x => MapValid(x, currentTime)));
    }

    private static ValidTaskModel MapValid(ToDoTask task, DateTime currentTime)
    {
        return new ValidTaskModel(
            task.Id,
            task.Title,
            task.Description,
            task.CreationDate,
            task.ValidUntil,
            task.IsAboutToExpire(currentTime));
    }
}
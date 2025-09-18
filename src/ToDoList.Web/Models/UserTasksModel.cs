namespace ToDoList.Web.Models;

public sealed record UserTasksModel(
    List<TaskModel> ValidTasks,
    List<TaskModel> ExpiredTasks,
    List<TaskModel> CompletedTasks)
{
    public static UserTasksModel Empty()
    {
        return new UserTasksModel([], [], []);
    }
}
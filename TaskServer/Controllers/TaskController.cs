using Microsoft.AspNetCore.Mvc;
using TaskServer.Models;
using TaskServer.Repository.Interface;

namespace TaskServer.Controllers;

[Route("[controller]")]
public class TaskController : Controller
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    [HttpGet("active")]
    public IActionResult ActiveTask(string token)
    {
        var activeTasks = _taskRepository.ActiveTasks_Select(token);
        return new JsonResult(activeTasks);
    }

    [HttpGet("done")]
    public IActionResult DoneTask(string token)
    {
        var doneTasks = _taskRepository.DoneTasks_Select(token);
        return new JsonResult(doneTasks);
    }

    [HttpGet("insert")]
    public IActionResult Insert(string token, string title, string description)
    {
        var newTaskId = _taskRepository.ActiveTask_Insert(token, new ActiveTask{Title = title, Description = description});
        return new JsonResult(new Dictionary<string, long?>{{"insertedTaskID", newTaskId}});
    }
}
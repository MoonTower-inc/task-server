using TaskServer.Models;

namespace TaskServer.Repository.Interface;

public interface ITaskRepository
{
    public List<ActiveTask> ActiveTasks_Select(string token);

    public List<DoneTask> DoneTasks_Select(string token);

    public void ActiveTask_Insert(string token);
}
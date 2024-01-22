using TaskServer.Models;

namespace TaskServer.Repository.Interface;

public interface ITaskRepository
{
    public List<ActiveTask> ActiveTasks_Select(string token);

    public List<DoneTask> DoneTasks_Select(string token);

    public long? ActiveTask_Insert(string token, ActiveTask task);

    public bool Task_Update(string token, ActiveTask task);

    public bool Task_Delete(string token, long taskId);

    public bool Task_MoveToDone(string token, long[] taskIds);
}
using Npgsql;
using TaskServer.Models;
using TaskServer.Repository.Interface;

namespace TaskServer.Repository;

public class TaskRepository : RepositoryBase, ITaskRepository
{
    private string _connectionString;

    public TaskRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public List<ActiveTask> ActiveTasks_Select(string token)
    {
        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var connection = dataSource.OpenConnection())
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM task.activetask_select(@_token)", connection)
                       {
                           Parameters = { safeNpgsqlParameter("_token", token) }
                       })
                {
                    var reader = cmd.ExecuteReader();

                    var tasks = new List<ActiveTask>();

                    while (reader.Read())
                    {
                        var tempTask = new ActiveTask
                        {
                            Id = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2)
                        };
                        tasks.Add(tempTask);
                    }

                    return tasks;
                };
            }   
        }
    }

    public List<DoneTask> DoneTasks_Select(string token)
    {
        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var connection = dataSource.OpenConnection())
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM task.donetasks_select(@_token)", connection)
                       {
                           Parameters = { safeNpgsqlParameter("_token", token) }
                       })
                {
                    var reader = cmd.ExecuteReader();

                    var tasks = new List<DoneTask>();

                    while (reader.Read())
                    {
                        var tempTask = new DoneTask
                        {
                            Id = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            DoneDate = DateOnly.FromDateTime(reader.GetDateTime(3))
                        };
                        tasks.Add(tempTask);
                    }

                    return tasks;
                };
            }   
        }
    }

    public long? ActiveTask_Insert(string token, ActiveTask task)
    {
        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var connection = dataSource.OpenConnection())
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM task.task_insert(@_token, @_title, @_description)", connection)
                       {
                           Parameters =
                           {
                               safeNpgsqlParameter("_token", token),
                               safeNpgsqlParameter("_title", task.Title),
                               safeNpgsqlParameter("_description", task.Description),
                           }
                       })
                {
                    var newId = cmd.ExecuteScalar() as long?;

                    return newId;
                };
            }   
        }
    }

    public bool Task_Update(string token, ActiveTask task)
    {
        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var connection = dataSource.OpenConnection())
            {
                using (var cmd = new NpgsqlCommand("select * from task.task_update(@_token, @_taskid, @_title, @_description);", connection)
                       {
                           Parameters =
                           {
                               safeNpgsqlParameter("_token", token),
                               safeNpgsqlParameter("_taskid", task.Id),
                               safeNpgsqlParameter("_title", task.Title),
                               safeNpgsqlParameter("_description", task.Description),
                           }
                       })
                {
                    var result = (bool)cmd.ExecuteScalar();
                    return result;
                };
            }   
        }

        return false;
    }
    
    public bool Task_Delete(string token, long taskId)
    {
        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var connection = dataSource.OpenConnection())
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM task.task_delete(@_token, @_id)", connection)
                       {
                           Parameters =
                           {
                               safeNpgsqlParameter("_token", token),
                               safeNpgsqlParameter("_id", taskId),
                           }
                       })
                {
                    var result = (bool)cmd.ExecuteScalar();
                    return result;
                };
            }   
        }
    }
    
    public bool Task_MoveToDone(string token, long[] taskIds)
    {
        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var connection = dataSource.OpenConnection())
            {
                string taskIdsStr = "";
                foreach (var taskId in taskIds)
                {
                    taskIdsStr += $"{taskId} ";
                }

                if (!String.IsNullOrEmpty(taskIdsStr))
                {
                    taskIdsStr = taskIdsStr.Remove(taskIdsStr.Length - 1, 1);
                }
                Console.WriteLine(taskIdsStr);
                using (var cmd = new NpgsqlCommand("SELECT * FROM task.movetodone(@_token, @_ids)", connection)
                       {
                           Parameters =
                           {
                               safeNpgsqlParameter("_token", token),
                               safeNpgsqlParameter("_ids", taskIdsStr),
                           }
                       })
                {
                    var result = (bool)cmd.ExecuteScalar();
                    return result;
                };
            }   
        }
    }
}
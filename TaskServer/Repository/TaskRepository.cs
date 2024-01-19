using System.Data;
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
                        var tempTask = new ActiveTask();
                        tempTask.Id = reader.GetInt64(0);
                        tempTask.Title = reader.GetString(1);
                        tempTask.Description = reader.GetString(2);
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
                        var tempTask = new DoneTask();
                        tempTask.Id = reader.GetInt64(0);
                        tempTask.Title = reader.GetString(1);
                        tempTask.Description = reader.GetString(2);
                        tempTask.DoneDate = DateOnly.FromDateTime(reader.GetDateTime(3));
                        tasks.Add(tempTask);
                    }

                    return tasks;
                };
            }   
        }
    }

    public void ActiveTask_Insert(string token)
    {
        throw new NotImplementedException();
    }
}
using Npgsql;
using TaskServer.Models;
using TaskServer.Repository.Interface;

namespace TaskServer.Repository;

public class AuthRepository : RepositoryBase, IAuthRepository
{
    private string _connectionString;

    public AuthRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public bool Register(string email, string password)
    {
        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var connection = dataSource.OpenConnection())
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM auth.register(@_email, @_password)", connection)
                       {
                           Parameters =
                           {
                               safeNpgsqlParameter("_email", email),
                               safeNpgsqlParameter("_password", password)
                           }
                       })
                {
                    var result =  (bool)(cmd.ExecuteScalar() ?? false);

                    return result;
                };
            }   
        }
    }

    public string? Login(string login, string password)
    {
        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var connection = dataSource.OpenConnection())
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM auth.login(@_login, @_password)", connection)
                       {
                           Parameters =
                           {
                               safeNpgsqlParameter("_login", login),
                               safeNpgsqlParameter("_password", password)
                           }
                       })
                {
                    var result = cmd.ExecuteScalar() as string;

                    return result;
                };
            }   
        }
    }
}
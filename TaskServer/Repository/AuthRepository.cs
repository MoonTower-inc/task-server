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
        throw new NotImplementedException();
    }

    public string Login(string email, string password)
    {
        throw new NotImplementedException();
    }
}
namespace TaskServer.Repository.Interface;

public interface IAuthRepository
{
    public bool Register(string email, string password);

    public string Login(string login, string password);
}
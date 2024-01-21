using Microsoft.AspNetCore.Mvc;
using TaskServer.Repository;
using TaskServer.Repository.Interface;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TaskServer.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost("register")]
    public IActionResult Register(string login, string password)
    {
        if (_authRepository.Register(login, password))
        {
            return new OkResult();
        }

        return new ConflictResult();
    }

    [HttpGet("login")]
    public IActionResult Login(string login, string password)
    {
        var token = _authRepository.Login(login, password);
        if (String.IsNullOrEmpty(token))
        {
            return new ConflictResult();
        }

        return new ObjectResult(token);
    }
}
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
    public IActionResult Register(string email, string password)
    {
        if (_authRepository.Register(email, password))
        {
            return new OkResult();
        }

        return new ConflictResult();
    }

    [HttpGet("login")]
    public IActionResult Login(string email, string password)
    {
        var token = _authRepository.Login(email, password);
        if (String.IsNullOrEmpty(token))
        {
            return new ConflictResult();
        }

        return new ObjectResult(token);
    }
}
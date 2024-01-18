using Microsoft.AspNetCore.Mvc;
using Integration.BancoBTG.Service.Models;
using Integration.BancoBTG.Service.Services;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authenticationService;

    public AuthController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        var user = _authenticationService.ValidateUser(login.Username, login.Password);
        if (user != null)
        {
            var token = _authenticationService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
        return Unauthorized("Credenciais inválidas.");
    }
}

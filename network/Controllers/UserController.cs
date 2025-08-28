using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using network.Models;
using network.R;
using network.Services;

namespace network.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

[HttpPost("register")]
public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
{
    var user = await userService.RegisterAsync(request);
    return user is not null ? Ok(user) : BadRequest("Registration failed");
}

[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
{
    var user = await userService.LoginAsync(request);
    return user is not null ? Ok(user) : BadRequest("Invalid email or password");
}}


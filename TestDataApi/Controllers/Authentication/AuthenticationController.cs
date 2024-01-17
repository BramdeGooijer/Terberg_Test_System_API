using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestDataApi.Contracts.Authentication;
using TestDataApi.Services.Authentication;

namespace TestDataApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(request.Username, request.Password);
        var response = new AuthenticationResponse(authResult.Id, authResult.Username, authResult.Token);
        return Ok(response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Username, request.Password);
        var response = new AuthenticationResponse(authResult.Id, authResult.Username, authResult.Token);
        return Ok(response);
    }
}
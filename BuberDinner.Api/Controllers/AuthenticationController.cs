using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        OneOf<AutenticationResult, IError> registerResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        return registerResult.Match(
            authResult => Ok(MapAuthReponse(authResult)),
            error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage)
        );
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);
        var response = new AuthenticationResponse(
            authResult.user.Id, 
            authResult.user.FirstName!, 
            authResult.user.LastName!, 
            authResult.user.Email!, 
            authResult.Token
        );
        return Ok(response);
    }

    private static AuthenticationResponse MapAuthReponse(AutenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.user.Id, 
            authResult.user.FirstName!, 
            authResult.user.LastName!, 
            authResult.user.Email!, 
            authResult.Token
        );
    }
}
using BuberDinner.Application.Common.Errors;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AutenticationResult> Register(string firstName, string lastName, string email, string password);
    ErrorOr<AutenticationResult> Login(string email, string password);
}

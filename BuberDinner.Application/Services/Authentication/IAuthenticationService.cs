using BuberDinner.Application.Common.Errors;
using OneOf;

namespace BuberDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    OneOf<AutenticationResult, DuplicatedEmailError> Register(string firstName, string lastName, string email, string password);
    AutenticationResult Login(string email, string password);
}

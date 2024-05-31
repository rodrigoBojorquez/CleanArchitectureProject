namespace BuberDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    AutenticationResult Register(string firstName, string lastName, string email, string password);
    AutenticationResult Login(string email, string password);
}

namespace BuberDinner.Application.Services.Authentication;

public record AutenticationResult(Guid Id, string FirstName, string LastName, string Email, string Token);
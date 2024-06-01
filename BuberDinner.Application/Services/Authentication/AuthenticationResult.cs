using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public record AutenticationResult(User user, string Token);
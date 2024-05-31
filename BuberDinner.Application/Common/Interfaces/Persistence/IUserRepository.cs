using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    // Task<User> GetByIdAsync(Guid id);
    void CreateUser(User user);
    // Task<User> UpdateAsync(User user);
    // Task DeleteAsync(Guid id);
}
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AutenticationResult Login(string email, string password)
    {
        /* 
            1.- Verificar que el usuario exista en la base de datos
            2.- Verificar que la contraseña sea correcta
            3.- Generar un token para el usuario
        */

        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email does not exists");
        }

        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName!, user.LastName!);

        return new AutenticationResult(Guid.NewGuid(), user.FirstName, user.LastName, email, token);
    }

    public AutenticationResult Register(string firstName, string lastName, string email, string password)
    {
        /* 
            1.- Verificar que el usuario no exista en la base de datos
            2.- Crear al usuario y generar su id
            3.- Generar un token para el usuario
        */

        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User already exists");
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.CreateUser(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, firstName, lastName);

        return new AutenticationResult(user.Id, firstName, lastName, email, token);
    }
}
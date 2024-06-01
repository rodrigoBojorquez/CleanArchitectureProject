using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using OneOf;

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
            throw new Exception("Invalid credentials");
        }

        if (user.Password != password)
        {
            throw new Exception("Invalid credentials");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AutenticationResult(user, token);
    }

    public OneOf<AutenticationResult, DuplicatedEmailError> Register(string firstName, string lastName, string email, string password)
    {
        /* 
            1.- Verificar que el usuario no exista en la base de datos
            2.- Crear al usuario y generar su id
            3.- Generar un token para el usuario
        */

        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return new DuplicatedEmailError();
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.CreateUser(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AutenticationResult(user, token);
    }
}
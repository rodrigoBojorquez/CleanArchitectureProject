using System.Net;

namespace BuberDinner.Application.Common.Errors;

public record struct DuplicatedEmailError() : IError
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "The email is already in use";
}

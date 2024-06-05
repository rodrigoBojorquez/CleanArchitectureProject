using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors;

/* 
    TRequest : mediator request
    TResponse : error or response
*/

public class ValidationBehavior<TRequest, TResponse> : 
    IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    /* 
        Se tiene que declarar explicitamente que el validador puede ser nulo,
        de otra forma da un error de compilacion
    */

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    /* 
        Comportamiento para todas las peticiones, si no hay un error de validacion
        se continua con el flujo, de otra forma se retorna una lista de errores
    */

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator == null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage))
                .ToList();

            return (dynamic)errors;
        }

        return await next();
    }
}
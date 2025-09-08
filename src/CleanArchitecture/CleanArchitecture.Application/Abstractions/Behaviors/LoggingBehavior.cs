using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace CleanArchitecture.Application.Abstractions.Behaviors;

public class LoginBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse> 
where TRequest : IBaseRequest
where TResponse: Result
{
    private readonly ILogger<LoginBehavior<TRequest,TResponse>> _logger;

    public LoginBehavior(ILogger<LoginBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Ejecutando el request {name}", name);
            var result = await next();

            if (result.IsSuccess)
            {
                _logger.LogInformation($"El request {name} fue exitoso", name);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                     _logger.LogError($"El request {name} tiene errores", name);
                }
               
            }

            _logger.LogInformation($"El request {name} se ejecutó exitosamente", name);
            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"El request {name} tuvo errores",name);
            throw;
        }      
    }
}




    

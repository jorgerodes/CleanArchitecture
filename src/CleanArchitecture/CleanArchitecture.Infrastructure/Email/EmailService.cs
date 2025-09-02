using CleanArchitecture.Application.Abstractions.Email;

namespace CleanArchitecture.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
    {
        //! TODO: Envía un email con Gmail o cualquier otro proveedor
        return Task.CompletedTask;

    }
}
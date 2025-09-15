
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.users.Events;
using CleanArchitecture.Domain.Users;
using MediatR;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreateDomainEvent>
{

    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserCreatedDomainEventHandler(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(UserCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }
        
        await _emailService.SendAsync(
            user.Email!,
            "Se ha creado su cuenta en la app",
            $"Hello {user.Nombre?.Value}, welcome to Clean Architecture!"
            );
        
    }
}

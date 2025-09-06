using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        // 1. Validar que usuario no exista en BBDD
        var email = new Email(request.Email);
        var userExists = await _userRepository.IsUserExists(email);

        if (userExists)
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExists);
        }

        //2. encriptar el password

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // 3. crear objeto de tipo user

        var user = User.Create(
            new Nombre(request.Nombre),
            new Apellido(request.Apellidos),
            new Email(request.Email),
            new PasswordHash(passwordHash)
        );

        // 4 . insertar user en BBDD

        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync();

        return user.Id!.Value;
    }
}

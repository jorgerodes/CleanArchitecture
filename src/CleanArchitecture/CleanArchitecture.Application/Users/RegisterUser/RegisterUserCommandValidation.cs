using FluentValidation;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
        RuleFor(c => c.Apellidos).NotEmpty().WithMessage("Los apellidos son obligatorios");
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(5);
    
    }
}
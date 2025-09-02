using FluentValidation;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;


public class ReservarAlquilerCommandValidator : AbstractValidator<ReservarAlquilerCommand>
{
    public ReservarAlquilerCommandValidator()
    {
        RuleFor(c => c.userId).NotEmpty();
        RuleFor(c => c.vehiculoId).NotEmpty();
        RuleFor(c => c.fechaInicio).LessThan(c => c.fechaFin);
    }
}


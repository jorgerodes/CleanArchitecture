using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;

namespace CleanArchitecture.Domain.Alquileres;

public static class AlquilerErrors
{

    public static readonly Error NotFound = new(
        "Alquiler.Found",
        "No se encontró el alquiler con el id especificado."
    );

    public static readonly Error Overlap = new(
        "Alquiler.Overlap",
        "El vehículo ya está alquilado en el rango de fechas especificado."
    );

    public static readonly Error NotReserved = new(
        "Alquiler.NotReserved",
        "El alquiler no está en estado reservado."
    );
    public static readonly Error NotConfirmed = new(
        "Alquiler.NotConfirmed",
        "El alquiler no está en estado confirmado."
    );
    public static readonly Error AlreadyStarted = new(
        "Alquiler.AlreadyStarted",
        "El alquiler ya ha comenzado."
    );

}
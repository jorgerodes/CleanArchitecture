namespace CleanArchitecture.Domain.Alquileres;

public sealed record DateRange
{
    private DateRange()
    {

    }
    public DateOnly inicio { get; init; }
    public DateOnly fin { get; init; }

    public int CantidadDias => fin.DayNumber - inicio.DayNumber;

    public static DateRange Create(DateOnly inicio, DateOnly fin)
    {
        if (inicio >  fin)
        {
            throw new ApplicationException("La fecha de fin es anterior a la fecha de inicio.");
        }

        return new DateRange
        {
            inicio = inicio,
            fin = fin
        };
    }
}
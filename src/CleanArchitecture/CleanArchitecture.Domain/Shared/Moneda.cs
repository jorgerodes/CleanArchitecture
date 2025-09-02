
namespace CleanArchitecture.Domain.Shared;

public record Moneda(decimal monto, TipoMoneda TipoMoneda)
{

    public static Moneda operator +(Moneda primero, Moneda segundo)
    {
        if (primero.TipoMoneda != segundo.TipoMoneda)
        {
            throw new InvalidOperationException("Cannot add Monedas with different TipoMonedas.");
        }
        return new Moneda(primero.monto + segundo.monto, primero.TipoMoneda);
    }

    public static Moneda Zero() => new Moneda(0, TipoMoneda.None);
    public static Moneda Zero(TipoMoneda tipoMoneda) => new Moneda(0, tipoMoneda);
    public bool IsZero => this == Zero(TipoMoneda);
};
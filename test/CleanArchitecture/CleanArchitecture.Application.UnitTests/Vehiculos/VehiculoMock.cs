using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.UnitTests.Vehiculos;
internal static class VehiculoMock
{
    public static Vehiculo Create()
    {
        return new Vehiculo
        (
            VehiculoId.New(),
            new Modelo("Civic"),
            new Vin("1HGBH41JXMN109186"),
            new Moneda(150.0m, TipoMoneda.Usd),
            Moneda.Zero(),
            DateTime.UtcNow,
            [],
            new Direccion("USA","Texas","Houston","El Paso","77001")
        );

    }
}

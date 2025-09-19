using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.UnitTests.Vehiculos;
internal static class VehiculoMock
{
    public static Vehiculo Create(Moneda precio, Moneda? mantenimiento = null)
    {
        return new Vehiculo
        (
            VehiculoId.New(),
            new Modelo("Civic"),
            new Vin("1HGBH41JXMN109186"),
            precio,
            mantenimiento ?? Moneda.Zero(precio.TipoMoneda),
            DateTime.UtcNow.AddYears(-1),
            [],
            new Direccion("USA","Texas","Houston","El Paso","77001")
        );

    }
}

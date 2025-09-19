
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.UnitTests.Infrastructure;
using CleanArchitecture.Domain.UnitTests.Users;
using CleanArchitecture.Domain.UnitTests.Vehiculos;
using FluentAssertions;
using Xunit;
using DomainUser = CleanArchitecture.Domain.Users.User;
namespace CleanArchitecture.Domain.UnitTests.Alquileres;

public class AlquilerTest : BaseTest
{

    [Fact]
    public void Reserva_Should_RaiseAlquilerReservaDomainEvent()
    {

        // Arrange
        var user = DomainUser.Create(
            UserMock.Nombre,
            UserMock.Apellido,
            UserMock.Email,
            UserMock.Password
        );

        var precio = new Moneda(100.0m, TipoMoneda.Usd);
        var duracion = DateRange.Create(
            new DateOnly(2024, 1, 1),
            new DateOnly(2025, 1, 1)
        );
        var vehiculo = VehiculoMock.Create(precio);
        var precioService = new PrecioService();

        //Act
        var alquiler = Alquiler.Reservar(
            vehiculo,
            user.Id!,
            duracion,
            DateTime.UtcNow,
            precioService
        );


        //Assert

        var alquilerReservaDomainEvent = AssertDomainEventWasPublished<AlquilerReservadoDomainEvent>(alquiler);

        alquilerReservaDomainEvent.AlquilerId.Should().Be(alquiler.Id);

    }
    }

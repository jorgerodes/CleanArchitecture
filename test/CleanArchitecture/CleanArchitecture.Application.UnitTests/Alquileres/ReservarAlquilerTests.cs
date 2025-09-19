using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.UnitTests.Users;
using CleanArchitecture.Application.UnitTests.Vehiculos;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Design;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Alquileres;

public class ReservarAlquilerTests
{


   private readonly ReservarAlquilerCommandHandler _handlerMock;

   private readonly IUserRepository _userRepositoryMock;
   private readonly IVehiculosRepository _vehiculoRepositoryMock;
   private readonly IAlquilerRepository _alquilerRepositoryMock;

   private readonly IUnitOfWork _unitOfWorkMock;
   private readonly DateTime UtcNow = DateTime.UtcNow;

   private readonly ReservarAlquilerCommand command = new(
      Guid.NewGuid(),
      Guid.NewGuid(),
      new DateOnly(2024, 1, 1),
      new DateOnly(2025, 1, 1)
   );

   public ReservarAlquilerTests()
   {
      _userRepositoryMock = Substitute.For<IUserRepository>();
      _vehiculoRepositoryMock = Substitute.For<IVehiculosRepository>();
      _alquilerRepositoryMock = Substitute.For<IAlquilerRepository>();
      _unitOfWorkMock = Substitute.For<IUnitOfWork>();
      IDateTimeProvider dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
      dateTimeProviderMock.currentTime.Returns(UtcNow);

      _handlerMock = new ReservarAlquilerCommandHandler(
         _userRepositoryMock,
         _vehiculoRepositoryMock,
         _alquilerRepositoryMock,
         new PrecioService(),
         _unitOfWorkMock,
         dateTimeProviderMock
      );



   }

   [Fact]
   public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
   {
      //Arrange
      _userRepositoryMock.GetByIdAsync(
         new UserId(command.userId),
          Arg.Any<CancellationToken>()
      )
      .Returns((User?)null);

      //Act
      var resultado = await _handlerMock.Handle(command, default);
      //Assert

      resultado.Error.Should().Be(UserErrors.NotFound);
   }
   [Fact]
   public async Task Handle_Should_ReturnFailure_WhenVehiculoIsNull()
   {

      //Arrange

      var user = UserMock.Create();

      _userRepositoryMock.GetByIdAsync(
         new UserId(command.userId),
          Arg.Any<CancellationToken>()
      )
      .Returns(user);

      _vehiculoRepositoryMock.GetByIdAsync(
         new VehiculoId(command.vehiculoId),
          Arg.Any<CancellationToken>()
      ).Returns((Vehiculo?)null);

      //Act
      var resultado = await _handlerMock.Handle(command, default);
      //Assert

      resultado.Error.Should().Be(VehiculoErrors.NotFound);

   }

   [Fact]
   public async Task Handle_Should_ReturnFailure_WhenVehiculoIsAlquilado()
   {
      //Arrange

      var user = UserMock.Create();
      var vehiculo = VehiculoMock.Create();
      var duracion = DateRange.Create(command.fechaInicio, command.fechaFin);

      _userRepositoryMock.GetByIdAsync(
         new UserId(command.userId),
          Arg.Any<CancellationToken>()
      )
      .Returns(user);


      _vehiculoRepositoryMock.GetByIdAsync(
         new VehiculoId(command.vehiculoId),
          Arg.Any<CancellationToken>()
      )
      .Returns(vehiculo);

      _alquilerRepositoryMock.IsOverlappingAsync(
        vehiculo,
        duracion,
        Arg.Any<CancellationToken>())
        .Returns(true);
      //Act
      var resultados = await _handlerMock.Handle(command, default);
      //Assert
      resultados.Error.Should().Be(AlquilerErrors.Overlap);


   }



    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenAlquilerIsReservado()
    {
        //Arrange
        var user = UserMock.Create();
        var vehiculo = VehiculoMock.Create();
        var duracion = DateRange.Create(command.fechaInicio, command.fechaFin);

        _userRepositoryMock.GetByIdAsync(
            new UserId(command.userId),
            Arg.Any<CancellationToken>()
        ).Returns(user);

        _vehiculoRepositoryMock.GetByIdAsync(
            new VehiculoId(command.vehiculoId),
            Arg.Any<CancellationToken>()
        ).Returns(vehiculo);

        _alquilerRepositoryMock.IsOverlappingAsync(
            vehiculo,
            duracion,
            Arg.Any<CancellationToken>()
        ).Returns(false);

        //Act
        var resultado = await _handlerMock.Handle(command, default);

        //Assert
        resultado.IsSuccess.Should().BeTrue();

    }



}

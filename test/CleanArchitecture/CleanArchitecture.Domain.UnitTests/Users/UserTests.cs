
using Xunit;
using DomainUser = CleanArchitecture.Domain.Users.User;
using FluentAssertions;
using CleanArchitecture.Domain.users.Events;
using CleanArchitecture.Domain.UnitTests.Infrastructure;
using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Domain.UnitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        //Arrange -- > crear mock file - UserMock.cs

        //Act

        var user = DomainUser.Create(
            UserMock.Nombre,
            UserMock.Apellido,
            UserMock.Email,
            UserMock.Password
        );

        //Assert
        user.Nombre.Should().Be(UserMock.Nombre);
        user.Apellido.Should().Be(UserMock.Apellido);
        user.Email.Should().Be(UserMock.Email);
        user.PasswordHash.Should().Be(UserMock.Password);

    }

    [Fact]
    public void Create_Should_RaiseUserCreateDomainEvent()
    {
        //Arrange

        //Act
        var user = DomainUser.Create(
            UserMock.Nombre,
            UserMock.Apellido,
            UserMock.Email,
            UserMock.Password
        );

        //Assert
        //var domainEvent = user.GetDomainEvents().OfType<UserCreateDomainEvent>().SingleOrDefault();

        var domainEvent = AssertDomainEventWasPublished<UserCreateDomainEvent>(user);
        domainEvent!.UserId.Should().Be(user.Id);
    }

    [Fact]
    public void Create_Should_AddRegisterRoleToUser()
    {
        //Arrange

        //Act
        var user = DomainUser.Create(
            UserMock.Nombre,
            UserMock.Apellido,
            UserMock.Email,
            UserMock.Password
        );
        //Assert
        user.Roles.Should().Contain(Role.Cliente);
    }

}

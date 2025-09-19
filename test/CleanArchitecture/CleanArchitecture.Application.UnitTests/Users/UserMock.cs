
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.UnitTests.Users;

public static class UserMock
{
    public static User Create() => User.Create(Nombre, Apellido, Email, Password);
    public static readonly Nombre Nombre = new Nombre("John");
    public static readonly Apellido Apellido = new Apellido("Doe");
    public static readonly Email Email = new Email("johndoe@example.com");
    public static readonly PasswordHash Password = new PasswordHash("SecurePassword123!");

}

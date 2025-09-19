

using CleanArchitecture.Application.Users.RegisterUser;

namespace CleanArchitecture.Api.FunctionalTests.Users;
internal static class UserData
{
    public static RegisterUserRequest RegisterUserRequestTest =
                        new("jorge@gmail.com", "Jorge", "Jiménez Mainar", "jorgeTest5%@");
}

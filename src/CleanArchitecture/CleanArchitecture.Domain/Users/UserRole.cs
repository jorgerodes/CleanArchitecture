using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.User;

public sealed class UserRole
{
    public int RoleId { get; set; }
    public UserId? UserId { get; set; }
}
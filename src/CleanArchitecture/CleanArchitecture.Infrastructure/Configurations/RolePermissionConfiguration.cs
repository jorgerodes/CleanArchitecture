
using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("roles_permissions");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.Property(permission => permission.PermissionId)
            .HasConversion(permissionId => permissionId!.Value, value => new PermissionId(value));

        builder.HasData(
            Create(Role.Cliente, PermissionEnum.ReadUser),
            Create(Role.Admin, PermissionEnum.ReadUser),
            Create(Role.Admin, PermissionEnum.WriteUser),
            Create(Role.Admin, PermissionEnum.UpdateUser)
        );
    }

    private static RolePermission Create(Role role, PermissionEnum permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = new PermissionId((int)permission)
        };
    
    }
}
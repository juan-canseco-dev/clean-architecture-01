using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Roles;

public sealed class Role : Enumeration<Role>
{
    public static Role Admin = new(1, "Admin");
    public static Role Cliente = new(2, "Cliente");
    public ICollection<Permission>? Permissions { get; set; }
    public Role(int id, string name) : base(id, name) {}
}

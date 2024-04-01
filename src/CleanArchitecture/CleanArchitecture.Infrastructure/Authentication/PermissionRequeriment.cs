using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Infrastructure.Authentication;

public class PermissionRequeriment : IAuthorizationRequirement
{
    public string Permission { get;  }
    public PermissionRequeriment(string permission)
    {
        Permission = permission;
    }
}

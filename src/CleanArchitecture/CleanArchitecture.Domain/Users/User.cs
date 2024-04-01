using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users.Events;

namespace CleanArchitecture.Domain.Users;

public sealed class User : Entity<UserId>
{
    private readonly List<Role> _roles = new();

    public Nombre? Nombre {get; private set;}
    public Apellido? Apellido {get; private set;}
    public Email? Email {get; private set;}
    public PasswordHash? PasswordHash { get; private set; }

    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    private User() : base() { }

    private User(UserId id, Nombre nombre, Apellido apellido, Email email, PasswordHash passwordHash, List<Role> roles) : base(id) 
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        PasswordHash = passwordHash;
        _roles.AddRange(roles);
    }

    public static User Create(Nombre nombre, Apellido apellido, Email email, PasswordHash passwordHash, Role role) {
        List<Role> userRoles = new() { role };
        var user =  new User(UserId.New(), nombre, apellido, email, passwordHash, userRoles);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
}

using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);
        var userExists = await _userRepository.IsUserExists(email, cancellationToken);
        if (!userExists) 
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExists);
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // By default user will be register with the role Cliente

        var user = User.Create(
            new Nombre(request.Nombre),
            new Apellido(request.Apellidos),
            new Email(request.Email),
            new PasswordHash(passwordHash),
            Role.Cliente
        );

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }

}

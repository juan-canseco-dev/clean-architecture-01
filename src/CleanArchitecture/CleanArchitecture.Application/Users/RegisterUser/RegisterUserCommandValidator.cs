
using FluentValidation;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator() 
    {
        RuleFor(c => c.Nombre).NotEmpty();
        RuleFor(c => c.Apellidos).NotEmpty();
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(5);
    }



}

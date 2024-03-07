using UserEmail = CleanArchitecture.Domain.Users.Email;

namespace CleanArchitecture.Application.Abstractions.Email;

public interface IEmailService 
{
    Task SendAsync(UserEmail recipient, string subject, string body);
}
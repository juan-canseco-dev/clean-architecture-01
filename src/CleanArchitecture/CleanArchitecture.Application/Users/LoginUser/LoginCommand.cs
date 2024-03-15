using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Users.LoginUser;

public record class LoginCommand(string Email, string Password) : ICommand<string>;
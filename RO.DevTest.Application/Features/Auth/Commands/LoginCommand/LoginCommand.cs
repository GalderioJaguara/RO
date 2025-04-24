using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommand : IRequest<LoginResponse> {
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public LoginCommand(string username, string password) {
        Username = username;
        Password = password;
    }
}

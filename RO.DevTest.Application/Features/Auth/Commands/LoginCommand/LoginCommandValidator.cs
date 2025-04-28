using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(login => login.Username)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo usuário precisa ser preenchido");

        RuleFor(login => login.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo senha precisa ser preenchido")
            .MinimumLength(6)
            .WithMessage("O campo senha precisa ter, pelo menos, 6 caracteres");
    }
}


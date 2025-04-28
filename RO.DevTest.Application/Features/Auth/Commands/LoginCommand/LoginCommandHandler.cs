using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Validate the login request
        LoginCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult);
        }

        // Find user by email (assuming username is email)
        var user = await _identityAbstractor.FindUserByEmailAsync(request.Username);

        if (user == null)
        {
            throw new BadRequestException("Invalid username or password");
        }

        // Attempt to sign in the user
        var signInResult = await _identityAbstractor.PasswordSignInAsync(user, request.Password);

        if (!signInResult.Succeeded)
        {
            throw new BadRequestException("Invalid username or password");
        }

        // Get user roles
        var roles = await _identityAbstractor.GetUserRolesAsync(user);

        // Generate authentication token
        var tokenResult = await _identityAbstractor.GenerateJwtTokenAsync(user, roles);

        return new LoginResponse
        {
            AccessToken = tokenResult,
            ExpirationDate = DateTime.UtcNow.AddHours(24), // Set an appropriate expiration time
            IssuedAt = DateTime.UtcNow,
            Roles = roles
        };
    }
}

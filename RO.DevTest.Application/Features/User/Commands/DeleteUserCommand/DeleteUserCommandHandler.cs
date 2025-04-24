using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand {
    public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, DeleteUserResult> {
        private readonly IIdentityAbstractor _identityAbstractor;

        public DeleteUserCommandHandler(IIdentityAbstractor identityAbstractor) {
            _identityAbstractor = identityAbstractor;
        }

        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
            if (string.IsNullOrEmpty(request.Id)) {
                throw new NotFoundException($"Id({request.Id}) not Found");
            }
            Domain.Entities.User? user = await _identityAbstractor.FindUserByIdAsync(request.Id);
            if (user == null) {
                throw new NotFoundException("User not found");
            }

            DeleteUserResult result = new DeleteUserResult(user);

            IdentityResult deleteResult = await _identityAbstractor.DeleteUser(user);

            if (!deleteResult.Succeeded) {
                throw new BadRequestException(deleteResult);
            }
            return result;
        }

    }
}

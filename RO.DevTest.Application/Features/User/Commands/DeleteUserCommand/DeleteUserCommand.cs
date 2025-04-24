using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand {
    public class DeleteUserCommand: IRequest<DeleteUserResult> {
        public string Id { get; set; } = string.Empty;
        public DeleteUserCommand(string id) 
        {
            Id = id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand {
    public class DeleteUserResult {

        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DeleteUserResult() { }
        public DeleteUserResult(Domain.Entities.User user) {
        Id = user.Id;
        UserName = user.UserName!;
        Email = user.Email!;
        Name = user.Name!;
        }
    }
}

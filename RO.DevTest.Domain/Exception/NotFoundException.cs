using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Domain.Exception {
    public class NotFoundException : ApiException {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public NotFoundException(IdentityResult result) : base(result) { }

        public NotFoundException(string error) : base(error) { }

        public NotFoundException(ValidationResult validationResult) : base(validationResult) { }

    }
}

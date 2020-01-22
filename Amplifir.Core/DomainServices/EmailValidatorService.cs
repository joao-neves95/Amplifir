using System;
using System.Collections.Generic;
using System.Text;
using Amplifir.Core.Interfaces;
using EmailValidation;

namespace Amplifir.Core.DomainServices
{
    public class EmailValidatorService : IEmailValidatorService
    {
        public bool IsValid(string email)
        {
            return EmailValidator.Validate( email );
        }
    }
}

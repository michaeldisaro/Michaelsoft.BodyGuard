using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Michaelsoft.BodyGuard.Common.Settings;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidatePassword : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
                                                    ValidationContext validationContext)
        {
            // init
            var options = validationContext.GetService(typeof(IOptions<CommonSettings>)) as IOptions<CommonSettings>;
            var passwordSettings = options?.Value.PasswordSettings;
            var password = value as string ?? "";
            
            // checks
            if (passwordSettings == null) 
                return ValidationResult.Success;
            if (passwordSettings.PasswordShouldContainCharacters && !Regex.IsMatch(password, "[a-zA-Z]+"))
                return new ValidationResult("Password should contain at least one alphabetic character.");
            if (passwordSettings.PasswordShouldContainNumbers && !Regex.IsMatch(password, "[0-9]+"))
                return new ValidationResult("Password should contain at least one numeric character.");
            if (passwordSettings.PasswordShouldContainAtLeastOneSymbol && !Regex.IsMatch(password, "[^a-zA-Z0-9]+"))
                return new ValidationResult("Password should contain at least one symbol.");
            if (passwordSettings.PasswordShouldVaryCase && !Regex.IsMatch(password, "([a-z][A-Z]+)|([A-Z][a-z]+)"))
                return new ValidationResult("Password should contain mixed case letters.");
            return ValidationResult.Success;
        }

    }
}
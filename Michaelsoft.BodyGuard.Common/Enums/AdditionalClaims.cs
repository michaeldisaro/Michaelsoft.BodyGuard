using System.Collections.Generic;

namespace Michaelsoft.BodyGuard.Common.Enums
{
    public class AdditionalClaims
    {

        public const string EmailAddress = "emailaddress";

        public const string Nickname = "nickname";

        public const string Name = "name";

        public const string Surname = "surname";

        public const string FullName = "fullname";

        public const string PhoneNumber = "phone";

        public static Dictionary<string, string> ToUserProperty { get; } = new Dictionary<string, string>
        {
            {EmailAddress, "EmailAddress"},
            {Nickname, "Nickname"},
            {Name, "Name"},
            {Surname, "Surname"},
            {FullName, "FullName"},
            {PhoneNumber, "PhoneNumber"}
        };

    }
}
namespace Michaelsoft.BodyGuard.Common.Settings
{
    public class PasswordSettings
    {

        public bool PasswordShouldContainCharacters { get; set; } = true;

        public bool PasswordShouldContainNumbers { get; set; } = true;

        public bool PasswordShouldContainAtLeastOneSymbol { get; set; } = true;

        public bool PasswordShouldVaryCase { get; set; } = true;

    }
}
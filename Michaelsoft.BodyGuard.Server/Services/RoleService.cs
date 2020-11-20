using System.Collections.Generic;
using Michaelsoft.BodyGuard.Common.Settings;
using Michaelsoft.BodyGuard.Server.Interfaces;
using Michaelsoft.BodyGuard.Server.Settings;
using Michaelsoft.Mailer.Extensions;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class RoleService : IRoleService
    {

        public const string Root = "root";

        public const string Admin = "admin";

        public const string Dpo = "dpo";

        public const string User = "user";

        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();

        public RoleService(IOptions<IdentitySettings> identitySettings)
        {
            Roles["Root"] = Root;
            Roles["Admin"] = Admin;
            Roles["Dpo"] = Dpo;
            Roles["User"] = User;
            foreach (var customRole in identitySettings.Value.CustomRoles)
                Roles[customRole.Capitalize()] = customRole;
        }

        public string this[string role]
        {
            get => Roles.ContainsKey(role) ? Roles[role] : null;
            set => Roles[role] = value;
        }

    }
}
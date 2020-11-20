using System;
using System.Collections.Generic;
using Michaelsoft.BodyGuard.Common.Settings;
using Michaelsoft.BodyGuard.Server.Interfaces;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class RoleService : IRoleService
    {

        public const string Root = "root";

        public const string Admin = "admin";

        public const string Dpo = "dpo";

        public const string User = "user";

        public List<string> Roles { get; set; } = new List<string>();

        public RoleService(IOptions<IdentitySettings> identitySettings)
        {
            Roles.Add(Root);
            Roles.Add(Dpo);
            Roles.Add(Admin);
            Roles.Add(User);
            foreach (var customRole in identitySettings.Value.CustomRoles)
                Roles.Add(customRole);
        }

        public string this[string role]
        {
            get => Roles.Contains(role) ? role : null;
            set => throw new InvalidOperationException();
        }

    }
}
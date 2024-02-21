using NetBox.Admin.Core.Security.Claims;

namespace NetBox.Admin.Core.Security.AuthPolicies;

public sealed class ApplicationAuthPolicy
{
    public sealed class UserPolicy
    {
        public const string Create = ApplicationClaimValues.User.Create;
        public const string View = ApplicationClaimValues.User.View;
        public const string Edit = ApplicationClaimValues.User.Edit;
        public const string Delete = ApplicationClaimValues.User.Delete;
    }

    public sealed class RolePolicy
    {
        public const string Create = ApplicationClaimValues.Role.Create;
        public const string View = ApplicationClaimValues.Role.View;
        public const string Delete = ApplicationClaimValues.Role.Delete;
        public const string UpdateRoleClaim = ApplicationClaimValues.Role.UpdateRoleClaim;
    }


    public sealed class Settings
    {
        public sealed class Services
        {
            public const string Create = ApplicationClaimValues.Settings.Service.Create;
            public const string View = ApplicationClaimValues.Settings.Service.View;
            public const string Delete = ApplicationClaimValues.Settings.Service.Delete;
            public const string Update = ApplicationClaimValues.Settings.Service.Update;
        }

        public sealed class ClientType
        {
            public const string Create = ApplicationClaimValues.Settings.ClientType.Create;
            public const string View = ApplicationClaimValues.Settings.ClientType.View;
            public const string Delete = ApplicationClaimValues.Settings.ClientType.Delete;
            public const string Update = ApplicationClaimValues.Settings.ClientType.Update;
        }

        public sealed class JobType
        {
            public const string Create = ApplicationClaimValues.Settings.JobType.Create;
            public const string View = ApplicationClaimValues.Settings.JobType.View;
            public const string Delete = ApplicationClaimValues.Settings.JobType.Delete;
            public const string Update = ApplicationClaimValues.Settings.JobType.Update;
        }

        public sealed class DesignSentByMode
        {
            public const string Create = ApplicationClaimValues.Settings.DesignSentByMode.Create;
            public const string View = ApplicationClaimValues.Settings.DesignSentByMode.View;
            public const string Delete = ApplicationClaimValues.Settings.DesignSentByMode.Delete;
            public const string Update = ApplicationClaimValues.Settings.DesignSentByMode.Update;
        }

    }

    public sealed class Advertisment
    {
        public const string Create = ApplicationClaimValues.Advertisment.Create;
        public const string View = ApplicationClaimValues.Advertisment.View;
        public const string Delete = ApplicationClaimValues.Advertisment.Delete;
    }

    public sealed class Job
    {
        public const string Create = ApplicationClaimValues.Job.Create;
        public const string View = ApplicationClaimValues.Job.View;
        public const string Delete = ApplicationClaimValues.Job.Delete;
        public const string Edit = ApplicationClaimValues.Job.Edit;
    }

}

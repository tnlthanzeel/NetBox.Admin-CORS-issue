namespace NetBox.Admin.Core.Security.Claims;

public sealed class ApplicationClaimValues
{
    public sealed class SuperAdmin
    {
        public const string All = "all";
    }

    public sealed class User
    {
        public const string Create = "user.create";
        public const string View = "user.view";
        public const string Edit = "user.edit";
        public const string Delete = "user.delete";
    }

    public sealed class Role
    {
        public const string Create = "role.create";
        public const string View = "role.view";
        public const string UpdateRoleClaim = "role.roleclaim.update";
        public const string Delete = "role.delete";
    }

    public sealed class Settings
    {
        public sealed class Service
        {
            public const string Create = "service.create";
            public const string View = "service.view";
            public const string Update = "service.update";
            public const string Delete = "service.delete";
        }

        public sealed class ClientType
        {
            public const string Create = "client.type.create";
            public const string View = "client.type.view";
            public const string Update = "client.type.update";
            public const string Delete = "client.type.delete";
        }


        public sealed class JobType
        {
            public const string Create = "job.type.create";
            public const string View = "job.type.view";
            public const string Update = "job.type.update";
            public const string Delete = "job.type.delete";
        }

        public sealed class DesignSentByMode
        {
            public const string Create = "design.sent.by.create";
            public const string View = "design.sent.by.view";
            public const string Update = "design.sent.by.update";
            public const string Delete = "design.sent.by.delete";
        }
    }

    public sealed class Advertisment
    {
        public const string Create = "advertisment.create";
        public const string View = "advertisment.view";
        public const string Delete = "advertisment.delete";
    }

    public sealed class Job
    {
        public const string Create = "job.create";
        public const string View = "job.view";
        public const string Delete = "job.delete";
        public const string Edit = "job.update";
    }
}

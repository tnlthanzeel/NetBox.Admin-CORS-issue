using NetBox.Admin.Core.Security.Claims;

namespace NetBox.Admin.Core.Security.ModulePermissions;

public sealed record PermissionSet(string DisplayName, string Key);

public sealed class AppModulePermissions
{

    public static IReadOnlyList<KeyValuePair<string, List<KeyValuePair<string, IReadOnlyList<PermissionSet>>>>> GetPermissionList()
    {
        IList<KeyValuePair<string, List<KeyValuePair<string, IReadOnlyList<PermissionSet>>>>> groupedPermission = [];

        groupedPermission.Add(new("Admin",
            [
                _userPermissions,
                _rolePermissions,
                _servicesPermissions,
                _clientTypePermissions,
                _jobTypePermissions,
                _designSentByModePermissions,
                _advertisementPermissions,
                _jobPermissions
             ]));

        groupedPermission.Add(new("Designer", []));

        return groupedPermission.AsReadOnly();
    }

    static IReadOnlyList<KeyValuePair<string, IReadOnlyList<PermissionSet>>> GetPermissionListInternal()
    {
        return new List<KeyValuePair<string, IReadOnlyList<PermissionSet>>>
        {
            _userPermissions,
            _rolePermissions,
            _servicesPermissions,
            _clientTypePermissions,
            _jobTypePermissions,
            _designSentByModePermissions,
            _advertisementPermissions,
            _jobPermissions
        }.AsReadOnly();
    }

    public static IReadOnlyList<string> GetPermissionKeys()
    {
        var permissionList = GetPermissionListInternal();
        var keys = permissionList.SelectMany(s => s.Value).Select(s => s.Key).ToList();

        return keys;
    }

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _userPermissions =
        new(key: "Users", value:
        [
            new PermissionSet("View", ApplicationClaimValues.User.View),
            new PermissionSet("Create", ApplicationClaimValues.User.Create),
            new PermissionSet("Edit", ApplicationClaimValues.User.Edit),
            new PermissionSet("Delete", ApplicationClaimValues.User.Delete),
        ]);

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _rolePermissions =
        new(key: "Roles", value:
        [
            new PermissionSet("View", ApplicationClaimValues.Role.View),
            new PermissionSet("Create", ApplicationClaimValues.Role.Create),
            new PermissionSet("Update", ApplicationClaimValues.Role.UpdateRoleClaim),
            new PermissionSet("Delete", ApplicationClaimValues.Role.Delete)
        ]);

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _servicesPermissions =
        new(key: "Services", value:
        [
            new PermissionSet("View", ApplicationClaimValues.Settings.Service.View),
            new PermissionSet("Create", ApplicationClaimValues.Settings.Service.Create),
            new PermissionSet("Update", ApplicationClaimValues.Settings.Service.Update),
            new PermissionSet("Delete", ApplicationClaimValues.Settings.Service.Delete)
        ]);

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _clientTypePermissions =
        new(key: "Client Types", value:
        [
            new PermissionSet("View", ApplicationClaimValues.Settings.ClientType.View),
            new PermissionSet("Create", ApplicationClaimValues.Settings.ClientType.Create),
            new PermissionSet("Update", ApplicationClaimValues.Settings.ClientType.Update),
            new PermissionSet("Delete", ApplicationClaimValues.Settings.ClientType.Delete)
        ]);

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _jobTypePermissions =
        new(key: "Job Types", value:
        [
            new PermissionSet("View", ApplicationClaimValues.Settings.JobType.View),
            new PermissionSet("Create", ApplicationClaimValues.Settings.JobType.Create),
            new PermissionSet("Update", ApplicationClaimValues.Settings.JobType.Update),
            new PermissionSet("Delete", ApplicationClaimValues.Settings.JobType.Delete)
        ]);

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _designSentByModePermissions =
        new(key: "Design Sent Mode", value:
        [
            new PermissionSet("View", ApplicationClaimValues.Settings.DesignSentByMode.View),
            new PermissionSet("Create", ApplicationClaimValues.Settings.DesignSentByMode.Create),
            new PermissionSet("Update", ApplicationClaimValues.Settings.DesignSentByMode.Update),
            new PermissionSet("Delete", ApplicationClaimValues.Settings.DesignSentByMode.Delete)
        ]);

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _advertisementPermissions =
        new(key: "Advertisements", value:
        [
            new PermissionSet("View", ApplicationClaimValues.Advertisment.View),
            new PermissionSet("Create", ApplicationClaimValues.Advertisment.Create),
            new PermissionSet("Delete", ApplicationClaimValues.Advertisment.Delete)
        ]);


    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _jobPermissions =
        new(key: "Jobs", value:
        [
            new PermissionSet("View", ApplicationClaimValues.Job.View),
            new PermissionSet("Create", ApplicationClaimValues.Job.Create),
            new PermissionSet("Update", ApplicationClaimValues.Job.Edit),
            new PermissionSet("Delete", ApplicationClaimValues.Job.Delete)
        ]);

}




namespace NetBox.Admin.SharedKernal;

public static class AppConstants
{
    public const string SriLankaTimeZone = "Sri Lanka Standard Time";

    public static class SuperAdmin
    {
        public static readonly Guid SuperUserId = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5");
        public static readonly Guid SuperAdminRoleId = Guid.Parse("e24f4cd1-0759-440e-9a2b-6072880392b6");
        public const string SuperAdminRoleName = "superadmin";
    }

    public static class Database
    {
        public const string APIDbConnectionName = "MSSQLDbConnection";
    }

    public static class StringLengths
    {
        public const int FirstName = 250;
        public const int LastName = 250;
        public const int Email = 256;
        public const int PhoneNumber = 20;
        public const int PostalCode = 128;
        public const int Address = 800;
        public const int State = 256;
        public const int City = 256;
        public const int Province = 256;
        public const int Notes = 1000;
        public const int Description = 1500;
    }


    public static class Administrator
    {
        public const string RoleName = "Administrator";
    }

    public static class QueueStorage
    {
        public static class QueueName
        {
            public const string EmailQueue = "email-queue";
        }
    }

    public static class FileExtension
    {
        public static readonly string[] ValidImageFileExtensions = [".jpeg", ".jpg", ".png"];
        public static readonly string[] ValidVideoFileExtensions = [".mp4", ".avi"];
        public static readonly string[] ValidAdvertisementFileExtensions = [.. ValidImageFileExtensions, .. ValidVideoFileExtensions];

    }

    public static class Folders
    {
        public const string DesignByModeIcons = "DesignByModeIcons";
        public const string Advertisements = "Advertisements";
    }


}

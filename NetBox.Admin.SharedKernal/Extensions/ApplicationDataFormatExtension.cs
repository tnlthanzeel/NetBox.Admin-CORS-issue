namespace NetBox.Admin.SharedKernal.Extensions;

public static class ApplicationDataFormatExtension
{
    public const string ApplicationDateFormat = "MM-dd-yyyy";

    public static string ToApplicationDateFormat(this DateTimeOffset? dateTime)
    {
        return dateTime?.ToString(ApplicationDateFormat) ?? "-";
    }

    public static string ToApplicationDateFormat(this DateTimeOffset dateTime)
    {
        return dateTime.ToString(ApplicationDateFormat);
    }

    public static string ToApplicationDateFormat(this DateTime dateTime)
    {
        return dateTime.ToString(ApplicationDateFormat);
    }
}

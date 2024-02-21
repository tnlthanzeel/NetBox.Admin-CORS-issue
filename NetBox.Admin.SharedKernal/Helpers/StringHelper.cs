using System.Text;

namespace NetBox.Admin.SharedKernal.Helpers;

public static class StringHelper
{
    public static StringBuilder RemoveStringBetween(StringBuilder builder, string startString, string endString)
    {
        var original = builder.ToString();
        int start = original.IndexOf(startString);
        int end = original.IndexOf(endString) + endString.Length;

        builder.Remove(start, end - start);

        return builder;
    }
}

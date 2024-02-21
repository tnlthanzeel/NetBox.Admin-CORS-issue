namespace NetBox.Admin.SharedKernal;

public static class AppEnums
{
    public enum JobStatus
    {
        None = 0,
        Queued,
        InProgress,
        Billed,
        Done
    }

    public enum DesignerJobProgress
    {
        None = 0,
        NotStarted,
        InProgress,
        Complated
    }

    public enum JobTimerStatus
    {
        None = 0,
        Started,
        Paused
    }


}

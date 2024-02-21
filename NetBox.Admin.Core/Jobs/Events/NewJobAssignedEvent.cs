namespace NetBox.Admin.Core.Jobs.Events;

sealed class NewJobAssignedEvent : DomainEventBase
{
    public DesignerAssignedJob DesignerAssignedJob { get; }

    public NewJobAssignedEvent(DesignerAssignedJob designerAssignedJob) : base(isPrePersistantDomainEvent: false)
    {
        DesignerAssignedJob = designerAssignedJob;
    }
}

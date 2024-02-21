using NetBox.Admin.SharedKernal.Models;

namespace NetBox.Admin.SharedKernal.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents, bool isPrePersistantDomainEvent);
}

using MediatR;
using Microsoft.AspNetCore.SignalR;
using NetBox.Admin.Core.Common.SignalR;
using NetBox.Admin.Core.Jobs.Events;

namespace NetBox.Admin.Core.Jobs.EventHandlers;

sealed class NotifyDesignerOfNewJobAssignmentHandler : INotificationHandler<NewJobAssignedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotifyDesignerOfNewJobAssignmentHandler(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(NewJobAssignedEvent notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients
                         .Group(notification.DesignerAssignedJob.AsigneeId.ToString())
                         .SendAsync(SignalRLClientMethods.NewJobAddedToDesigner);
    }
}

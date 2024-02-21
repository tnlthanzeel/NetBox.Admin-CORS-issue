using Microsoft.AspNetCore.SignalR;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Common.SignalR;

public sealed class NotificationHub : Hub
{
    private readonly ILoggedInUserService _loggedInUserService;

    public NotificationHub(ILoggedInUserService loggedInUserService)
    {
        _loggedInUserService = loggedInUserService;
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, _loggedInUserService.UserId);
        await base.OnConnectedAsync();
    }

    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, _loggedInUserService.UserId);
        await base.OnDisconnectedAsync(exception);
    }
}

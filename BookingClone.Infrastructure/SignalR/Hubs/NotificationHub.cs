using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.SignalR.Hubs
{
    [Authorize]
    public class NotificationHub : Hub //hub is a connection point 
    {
        /*When a client connects to a hub :
         * 1. A connection is established (over WebSockets, SSE, or long polling? )
         * 2. The server creates a Hub instance to handle messages for that connection - the instance is short-lived meaning it only lives while the connection is alive
         *     hubs are per connection/ not global singletons
         * 3. The method OnConnectedAsync() is automatically called on the server
         */
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            //Context is an object of type HubCallerContext
            //It holds info about the client connection that is currently talking to the hub
            //through Context we can access things like: 
            //-Context.ConnectionId => unique ID for that connection (everry browser tab / device has its own)
            //--
            //-Context.User => the ClaimsPrincipal representing the authenticated user
            //- Context.UserIdentifier => a shortcut string identifier for the user => By default, SignalR uses the ClaimTypes.NameIdentifier claim from the auth user to set UserIdentifier


            if (!string.IsNullOrEmpty(userId)) //checks whether user is authenticated/valid
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");
                //SignalR has a concept of groups: collection of connections you can broadcast messages to.
                //A group is just a named collection of connections. They are not stored in database unless we implement it ourself but they live in-memory while the connection is active
                //You can create as many groups as you want, and a single connection can belong to mtp groups
                //Groups are not created explicitly -- the first time you add a connection, SignalR created the group automatically 
                //
                //AddToGroupAsync(Context.ConnectionId, $"User_{userId}") => here we are telling SignalR 
                //  "Take this specific connectionId and add it to a group named User_{userId}

            }
            await base.OnConnectedAsync();
            //then we call base hub implementation (important so SignalR can run its own connection setup logic)
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
            //runs when a client disconnects
            //in case the connection dropped because of a network failure, server crash, timeout or some other error,
            //SignalR will provide an exception obj that explains why the disconnect happened
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{userId}");
            }
            await base.OnDisconnectedAsync(exception);
        }


        //we expose JoingGroup() and LeaveGroup() as hub methods so the client can control group membership in real time.
        //The point is to give clients a way to dynamically subscribe/unsubscribe to real-time message streams during their connection
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}

using Microsoft.AspNetCore.SignalR;

namespace Snai3i_API.Hubs
{
    public class ChatHub:Hub
    {

        public async Task SendMessage(string userId, string message)
        {
            //save in database code
            // Broadcast to all clients (can be customized to specific users)
            await Clients.All.SendAsync("ReceiveMessage", userId, message);//it want to tell all online clients that you have a receive message

        }


    }
}

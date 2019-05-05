using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Models.Models;
using Models.ViewModels;

namespace Logic
{
    [Authorize]
    public class MessageHub : Hub
    {
        // Connected IDs
        private static readonly HashSet<string> ConnectedIds = new HashSet<string>();

        public override async Task OnConnectedAsync()
        {
            ConnectedIds.Add(Context.ConnectionId);

            await Clients.All.SendAsync("Count", ConnectedIds.Count);

            await Clients.All.SendAsync("Receive", new RelayMessagePayload
            {
                From = "System",
                Text = "Welcome to the .NET workshop!",
                Time = DateTime.Now
            });
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            ConnectedIds.Remove(Context.ConnectionId);
            
            await Clients.All.SendAsync("Count", ConnectedIds.Count);
        }

        /// <summary>
        /// Sent the received message back to everyone
        /// </summary>
        /// <param name="receivedMessagePayload"></param>
        /// <returns></returns>
        public async Task RelayMessage(ReceivedMessagePayload receivedMessagePayload)
        {
            var payload = new RelayMessagePayload
            {
                From = Context.User.Identity.Name,
                Text = receivedMessagePayload.Text,
                Time = DateTime.Now
            };
            
            await Clients.All.SendAsync("ReceiveMessage", payload);
        }
    }
}
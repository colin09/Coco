namespace SR.Hubs {
    using System.Web;
    using System;
    using Microsoft.AspNet.SignalR;

    public class ChatHub : Hub {

        public void Send (string name, string message) {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage (name, message);
        }
    }
}
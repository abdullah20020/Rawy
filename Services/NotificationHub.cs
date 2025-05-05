using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; 
            Console.WriteLine($"User {userId} connected");
            return base.OnConnectedAsync();
        }
    }
}

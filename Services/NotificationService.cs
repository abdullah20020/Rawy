using core.Contracts.NotificationService;
using core.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repsotiry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private readonly RawyDbcontext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(RawyDbcontext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task NotifyAllUsersAsync(string message)
        {
            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                var notification = new Notification
                {
                    UserId = user.Id,
                    Message = message
                };

                _context.Notifications.Add(notification);

                // إرسال لحظي باستخدام SignalR
                await _hubContext.Clients.User(user.Id).SendAsync("ReceiveNotification", message);
            }

            await _context.SaveChangesAsync();
        }

        public async Task NotifyUserAsync(string userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
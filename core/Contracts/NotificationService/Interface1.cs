using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Contracts.NotificationService
{
    public interface INotificationService
    {
        Task NotifyAllUsersAsync(string message);
        Task NotifyUserAsync(string userId, string message);

    }
}

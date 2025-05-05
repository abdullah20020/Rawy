using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repsotiry.Data;
using Services;
using System.Security.Claims;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly IHubContext<NotificationHub> hubContext;
        private readonly RawyDbcontext rawyDbcontext;
        private readonly IGenaricrepostry<Notification> genaricrepostrynotification;

        public NotificationController( IHubContext<NotificationHub> hubContext, RawyDbcontext rawyDbcontext,IGenaricrepostry<Notification> genaricrepostrynotification)
        { 
            this.hubContext = hubContext;
            this.rawyDbcontext = rawyDbcontext;
            this.genaricrepostrynotification = genaricrepostrynotification;
        }
        [Authorize]
        [HttpGet("notifications")]
        public async Task<ActionResult<List<Notification>>> GetUserNotifications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            var notifications = await rawyDbcontext.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new Notification
                {
                    Message = n.Message,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync();

            return Ok(notifications);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await rawyDbcontext.Notifications.FindAsync(id);
            if (notification == null) return NotFound();

            rawyDbcontext.Notifications.Remove(notification);
            await rawyDbcontext.SaveChangesAsync();

            return Ok("Notification deleted");
        }
    }
}

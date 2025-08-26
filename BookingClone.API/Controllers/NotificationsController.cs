using BookingClone.API.Extensions;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead;
using BookingClone.Application.Features.Notifications.Commands.MarkNotificationAsRead;
using BookingClone.Application.Features.Notifications.Queries.GetUnreadNotifications;
using BookingClone.Application.Features.Notifications.Queries.GetUserNotifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    [Authorize]
    public class NotificationsController(ISender sender) : BaseAPIController
    {
        private readonly ISender _sender = sender;

        [HttpGet]
        public async Task<IActionResult> GetNotifications([FromQuery] NotificationsSearchParams searchParams)
        {
            searchParams = searchParams with { UserId = User.GetUserId() };
            var query = new GetUserNotificationsQuery
            {
                NotificationsSearchParams = searchParams
            };

            var result = await _sender.Send(query);

            Response.AddPaginationHeader(result.ValueOrDefault);

            return result.ToIActionResult();
        }

        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadNotifications([FromQuery] NotificationsSearchParams searchParams)
        {
            searchParams = searchParams with { UserId = User.GetUserId() };
            var query = new GetUserNotificationsQuery
            {
                NotificationsSearchParams = searchParams
            };

            var result = await _sender.Send(query);

            Response.AddPaginationHeader(result.ValueOrDefault);

            return result.ToIActionResult();
        }

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var command = new MarkNotificationAsReadCommand
            {
                NotificationId = id,
                UserId = User.GetUserId()
            };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }

        [HttpPatch("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var command = new MarkAllNotificationsAsReadCommand { UserId = User.GetUserId() };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Rumble.Platform.CalendarService.Models;
using Rumble.Platform.CalendarService.Services;
using Rumble.Platform.Common.Attributes;
using Rumble.Platform.Common.Web;
using Rumble.Platform.Data;

namespace Rumble.Platform.CalendarService.Controllers;

[ApiController, Route(template: "calendar/events"), RequireAuth]
public class EventController : PlatformController
{
#pragma warning disable
    private readonly EventService _events;
#pragma warning restore

    // Gets all events stored in Mongo
    [HttpGet, NoAuth, HealthMonitor(weight: 10)]
    public ObjectResult GetEvents() => Ok(new RumbleJson
    {
        { "events", _events.List() }
    });
}
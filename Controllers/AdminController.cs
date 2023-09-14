using Microsoft.AspNetCore.Mvc;
using Rumble.Platform.CalendarService.Models;
using Rumble.Platform.CalendarService.Services;
using Rumble.Platform.Common.Attributes;
using Rumble.Platform.Common.Utilities;
using Rumble.Platform.Common.Web;

namespace Rumble.Platform.CalendarService.Controllers;

[ApiController, Route(template: "calendar/admin"), RequireAuth(AuthType.ADMIN_TOKEN)]
public class AdminController : PlatformController
{
#pragma warning disable
    private readonly EventService _events;
#pragma warning restore
    
    // Adds in events to Mongo
    [HttpPost, Route("events"), HealthMonitor(weight: 100)]
    public ActionResult AddEvents() => Ok(new
    {
        eventsAdded = _events.BulkAdd(Require<Event[]>("events"))
    });
    
    // Clears out events
    [HttpDelete, Route("events"), HealthMonitor(weight: 10)]
    public ActionResult ClearEvents() => Ok(new
    {
        eventsCleared = _events.ClearAll()
    });
}
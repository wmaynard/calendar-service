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
    private readonly EventService _eventService;
#pragma warning restore
    
    // Adds in events to Mongo
    [HttpPost, Route("events"), HealthMonitor(weight: 100)]
    public async Task<ObjectResult> AddEvents()
    {
        List<Event> events = Require<List<Event>>(key: "events");

        long added = await _eventService.BulkAdd(events);

        return Ok(new
                  {
                      eventsAdded = added
                  });
    }
    
    // Clears out events
    [HttpDelete, Route("events"), HealthMonitor(weight: 10)]
    public async Task<ObjectResult> ClearEvents()
    {
        long cleared = await _eventService.ClearAll();

        return Ok(new
                  {
                      eventsCleared = cleared
                  });
    }
}
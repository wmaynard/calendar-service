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
    [HttpPost, Route("events")]
    public async Task<ObjectResult> AddEvents()
    {
        List<Event> events = Require<List<Event>>(key: "events");
        
        // TODO make sure a failed request doesn't end up clearing all and leaving the database empty
        long cleared = await _eventService.ClearAll();

        long added = await _eventService.BulkAdd(events);

        return Ok(new
                  {
                      eventsCleared = cleared,
                      eventsAdded = added
                  });
    }
}
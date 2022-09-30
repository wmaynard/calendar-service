using Microsoft.AspNetCore.Mvc;
using Rumble.Platform.CalendarService.Models;
using Rumble.Platform.CalendarService.Services;
using Rumble.Platform.Common.Attributes;
using Rumble.Platform.Common.Web;

namespace Rumble.Platform.CalendarService.Controllers;

[ApiController, Route(template: "calendar/events"), RequireAuth]
public class EventController : PlatformController
{
#pragma warning disable
    private readonly EventService _eventService;
#pragma warning restore

    // Gets all events stored in Mongo
    [HttpGet, NoAuth]
    public ObjectResult GetEvents()
    {
        List<Event> events = _eventService.List().ToList();

        return Ok(new {events});
    }
    
    // Adds in events to Mongo
    [HttpPost]
    public async Task<ObjectResult> AddEvents()
    {
        List<Event> events = Require<List<Event>>(key: "events");
        
        long cleared = await _eventService.ClearAll();

        long added = 0;
        foreach (Event ev in events)
        {
            _eventService.Create(ev);
            added++;
        }

        return Ok(new
                  {
                      eventsCleared = cleared,
                      eventsAdded = added
                  });
    }
}
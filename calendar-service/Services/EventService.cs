using Rumble.Platform.CalendarService.Models;
using Rumble.Platform.Common.Services;

namespace Rumble.Platform.CalendarService.Services;

public class EventService : PlatformMongoService<Event>
{
    public EventService() : base("events") {  }
}
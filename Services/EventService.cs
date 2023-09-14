using MongoDB.Driver;
using Rumble.Platform.CalendarService.Models;
using Rumble.Platform.Common.Minq;
using Rumble.Platform.Common.Services;

namespace Rumble.Platform.CalendarService.Services;

public class EventService : MinqService<Event>
{
    public EventService() : base("events") { }

    public long ClearAll() => mongo.All().Delete();

    public long BulkAdd(Event[] events)
    {
        mongo.Insert(events);
        return events.Length;
    }

    public Event[] List() => mongo.All().ToArray();
}
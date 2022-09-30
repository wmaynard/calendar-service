using MongoDB.Driver;
using Rumble.Platform.CalendarService.Models;
using Rumble.Platform.Common.Services;

namespace Rumble.Platform.CalendarService.Services;

public class EventService : PlatformMongoService<Event>
{
    public EventService() : base("events") {  }
    
    // Clears all existing events
    public async Task<long> ClearAll()
    {

        FilterDefinition<Event> filter = Builders<Event>.Filter.Empty;

        DeleteResult deleteResult = await _collection.DeleteManyAsync(filter);

        return deleteResult.DeletedCount;
    }
}
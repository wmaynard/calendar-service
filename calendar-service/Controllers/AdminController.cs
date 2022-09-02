using Microsoft.AspNetCore.Mvc;
using Rumble.Platform.CalendarService.Services;
using Rumble.Platform.Common.Attributes;
using Rumble.Platform.Common.Web;

namespace Rumble.Platform.CalendarService.Controllers;

[ApiController, Route(template: "calendar/events"), RequireAuth]
public class AdminController : PlatformController
{
#pragma warning disable
    private readonly EventService _eventService;
#pragma warning restore
    
    // TODO endpoint to update events based on csv
}
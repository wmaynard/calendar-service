using Microsoft.AspNetCore.Mvc;
using Rumble.Platform.Common.Web;

namespace Rumble.Platform.CalendarService.Controllers;

[ApiController, Route(template: "calendar")]
public class TopController : PlatformController
{
    // health handled by platform controller
}
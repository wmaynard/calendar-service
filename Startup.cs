using RCL.Logging;
using Rumble.Platform.Common.Web;

namespace Rumble.Platform.CalendarService;

public class Startup : PlatformStartup
{
    protected override PlatformOptions ConfigureOptions(PlatformOptions options) => options
         .SetProjectOwner(Owner.Nathan)
#if DEBUG
         .SetPerformanceThresholds(warnMS: 5_000, errorMS: 20_000, criticalMS: 300_000);
#else
        .SetPerformanceThresholds(warnMS: 500, errorMS: 2_000, criticalMS: 30_000);
#endif
}
using Microsoft.AspNetCore.Components;

namespace QuickGrid.Infrastructure;

[EventHandler("onclosecolumnoptions", typeof(EventArgs), enableStopPropagation: true, enablePreventDefault: true)]
public static class EventHandlers
{
}

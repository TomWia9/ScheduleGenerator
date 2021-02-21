using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ScheduleGenerator.Client.Helpers.ExtensionMethods
{
    public static class JSRuntimeExtensions
    {
        public static ValueTask<bool> Confirm(this IJSRuntime jsRuntime, string message)
        {
            return jsRuntime.InvokeAsync<bool>("confirm", message);
        }
    }
}

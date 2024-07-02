using System.Diagnostics;

namespace SlotGameServer.API.Middleware
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;

        public TimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopWatch = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                var elapsedMs = stopWatch.ElapsedMilliseconds;
                context.Response.Headers.Append("X-took", $"{elapsedMs} ms");

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}

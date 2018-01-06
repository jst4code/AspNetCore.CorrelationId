using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jst4Code.AspNetCore.CorrelationId
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private string _correlationIdHerder = "X-CorrelationId";

        public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(_correlationIdHerder, out var correlationId))
            {
                context.TraceIdentifier = correlationId;
            }

            // apply the correlation ID to the response header for client side tracking
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(_correlationIdHerder))
                {
                    context.Response.Headers.Add(_correlationIdHerder, context.TraceIdentifier);
                }

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}

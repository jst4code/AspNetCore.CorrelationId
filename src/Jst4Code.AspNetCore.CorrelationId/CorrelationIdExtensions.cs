using Jst4Code.AspNetCore.CorrelationId;
using Microsoft.AspNetCore.Builder;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorrelationIdExtensions
    {
        public static IApplicationBuilder AddCorrelationId(this IApplicationBuilder builder)
            => builder.UseMiddleware<CorrelationIdMiddleware>();
    }
}

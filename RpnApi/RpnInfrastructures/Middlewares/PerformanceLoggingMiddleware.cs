using Microsoft.AspNetCore.Http;
using RpnInfrastructures.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RpnInfrastructures.Middlewares
{
    /// <summary>
    /// Performance logging middleware records duration of each API operations.
    /// </summary>
    public class PerformanceLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiLogger _apiLogger;

        public PerformanceLoggingMiddleware(RequestDelegate next, IApiLogger apiLogger)
        {
            this._next = next;
            this._apiLogger = apiLogger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await _next(context);
            }
            finally 
            {
                sw.Stop();
                var request = context.Request;
                var parameters = new Dictionary<string, object>();
                parameters.Add(nameof(request.Method), request.Method);
                parameters.Add(nameof(request.Path), request.Path);
                parameters.Add(nameof(context.Response.StatusCode), context.Response?.StatusCode);
                _apiLogger.Performance("Request is processed.", sw.ElapsedMilliseconds, parameters);
            }
        }
    }
}

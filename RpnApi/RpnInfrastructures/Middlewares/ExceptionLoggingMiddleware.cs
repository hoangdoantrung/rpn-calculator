using Microsoft.AspNetCore.Http;
using RpnInfrastructures.Logging;
using System;
using System.Threading.Tasks;

namespace RpnInfrastructures.Middlewares
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiLogger _apiLogger;

        public ExceptionLoggingMiddleware(RequestDelegate next, IApiLogger apiLogger)
        {
            this._next = next;
            this._apiLogger = apiLogger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _apiLogger.Error("Unhandled exception has ocurred.", ex);
            }
        }
    }
}

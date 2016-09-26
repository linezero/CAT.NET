using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatASPNETCore
{
    public class CatMiddleware
    {
        private readonly RequestDelegate _next;

        public CatMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var tran = CatExtensions.NewTransaction("CatMiddleware", "Middleware");
            try
            {
                var request = httpContext.Request;
                var url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString.Value}";
                CatExtensions.LogEvent("CatMiddleware", "URL", "0", url);
                await _next(httpContext);
                tran.Status = "0";
            }
            catch (Exception ex)
            {
                tran.SetStatus(ex);
                CatExtensions.LogError(ex);
                throw;
            }
            finally
            {
                tran.Complete();
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CatMiddlewareExtensions
    {
        public static IApplicationBuilder UseCatMiddleware(this IApplicationBuilder builder)
        {
            CatExtensions.Init();
            return builder.UseMiddleware<CatMiddleware>();
        }
    }
}

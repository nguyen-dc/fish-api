using FLS.ServerSide.Model.Scope;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FLS.ServerSide.API.Systems
{
    public class ScopeContextMiddleware
    {
        private readonly RequestDelegate _next;

        public ScopeContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext httpContext,
            IScopeContext context
           )
        {
            context.AddHttpContext(httpContext);
            await _next(httpContext);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.SharedLibrary.Middlewares
{
    public class ListenToOnlyApiGateway(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Extract specific header from the request
            var signedHeader = context.Request.Headers["Api-Gateway"];

            // NULL means, the request is not coming from the Api Gateway // 503 serlice unavaliable
            if (signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sorry, service unavailable.");
                return;
            }
            else
            {
                await next(context);
            }

        }
    }
}

using App.Common.Compability.Middlewares;
using Microsoft.AspNetCore.Http;

namespace App.Common.Campatibility.Extensions
{
    public static class HttpContextExtensions
    {
        public static void ForceLogAsMaxInformation(this HttpContext context)
        {
            if (!context.Items.ContainsKey(LogRequestMiddleware.ForceLogRequestAsMaxInformation))
                context.Items.Add(LogRequestMiddleware.ForceLogRequestAsMaxInformation, null);
        }
    }
}

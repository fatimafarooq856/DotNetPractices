using App.Common.Campatibility.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System.Diagnostics;

namespace App.Common.Compability.Middlewares
{
    public class LogRequestMiddleware
    {
        public const string ForceLogRequestAsMaxInformation = "App_ForceLogRequestAsInformation";

        private readonly RequestDelegate _next;
        const string MessageTemplate = "User {UserId} requested {RequestMethod} {RequestPath} {RequestQuery} responded {StatusCode} in {Elapsed} ms";

        private static readonly ILogger Logger = Log.ForContext<LogRequestMiddleware>();

        public LogRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            var isException = false;
            try
            {
                await _next(context);
            }
            catch
            {
                isException = true;
                throw;
            }
            finally
            {
                sw.Stop();

                if (context.Request.Method != "OPTIONS" && !context.Items.ContainsKey(SkipLoggingAttribute.SkipLoggingAttributeItemKey))
                {
                    var elapsed = Math.Round(sw.Elapsed.TotalMilliseconds, 2);

                    var statusCode = context.Response?.StatusCode;
                    var level = statusCode >= 500 || isException ? LogEventLevel.Error : LogEventLevel.Information;
                    if (level > LogEventLevel.Information && context.Items.ContainsKey(ForceLogRequestAsMaxInformation)) level = LogEventLevel.Information;

                    var userId = context.User.GetUserIdIfExisting();
                    var requestMethod = context.Request.Method;
                    var requestPath = context.Request.Path;
                    var requestQuery = context.Request.Query;

                    Logger.Write(level, MessageTemplate, userId, requestMethod, requestPath, requestQuery, statusCode, elapsed);
                }
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Campatibility.Filters
{
	public class ValidateModelAttribute : ActionFilterAttribute
	{
		private static readonly ILogger Logger = Log.ForContext<ValidateModelAttribute>();

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var messages = new List<string>();
				foreach (var error in context.ModelState.Values.SelectMany(x => x.Errors))
				{
					if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
						messages.Add(error.ErrorMessage);

					if (!string.IsNullOrWhiteSpace(error.Exception?.Message))
						messages.Add(error.Exception.Message);
				}

				context.Result = new BadRequestObjectResult(new { messages });

#if DEBUG
				Logger.Write(LogEventLevel.Error, "{Message}", string.Join(Environment.NewLine, messages));
#endif
			}
		}
	}
}

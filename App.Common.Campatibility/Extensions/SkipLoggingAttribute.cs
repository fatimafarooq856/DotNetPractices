using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Campatibility.Extensions
{
	public class SkipLoggingAttribute : Attribute
	{
		public const string SkipLoggingAttributeItemKey = "App_SkipLogging";

		public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
		{
			context.HttpContext.Items.Add(SkipLoggingAttributeItemKey, null);
			await next();
		}
	}
}

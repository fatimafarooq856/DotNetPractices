using App.Common.Abstract.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NodaTime.Serialization.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Common.Campatibility.Configurations
{
	public static class ConfigureServices
	{
		public static IMvcBuilder ConfigureAppJsonOptions(this IMvcBuilder builder)
		{
			return builder.AddJsonOptions(configure =>
			{
				configure.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				configure.JsonSerializerOptions.IgnoreNullValues = true;
				configure.JsonSerializerOptions.Converters.Add(NodaTimeAppJsonConverters.InstantConverter);
				configure.JsonSerializerOptions.Converters.Add(NodaTimeAppJsonConverters.LocalDateTimeConverter);
				configure.JsonSerializerOptions.Converters.Add(NodaConverters.LocalDateConverter);
			});
		}
	}
}

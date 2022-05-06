using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utils.Extensions
{
	public static class StringExtensions
    {
		public static string AsBase64(this string str)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
		}

		public static string RemoveWhitespaceAndHyphens(this string str)
		{
			return str?.Replace("-", "").Replace(" ", "").Trim();
		}

		public static string AsNullIfEmptyOrTrim(this string str)
		{
			if (string.IsNullOrWhiteSpace(str)) return null;
			return str.Trim();
		}
	}
}

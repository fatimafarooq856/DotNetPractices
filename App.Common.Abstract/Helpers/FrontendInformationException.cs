using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Abstract.Helpers
{
	public class FrontendInformationException : Exception
	{
		public string OriginalMessage { get; }

		public FrontendInformationException(string message) : base(message)
		{
			OriginalMessage = message;
		}
	}
}

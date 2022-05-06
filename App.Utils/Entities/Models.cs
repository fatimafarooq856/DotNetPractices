using App.Common.Campatibility.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utils.Entities
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
	public class AppApiOptions
	{
		public bool UseHttps { get; set; }

		public string Instance { get; set; }
		public string Environment { get; set; }
		public string System { get; set; }

		//public Urls Urls { get; set; }

		public string AppFilesBaseFolder { get; set; }
		public string FileApiUrl { get; set; }

		//public SkatteverketOptions Skatteverket { get; set; }
		//public FortnoxOptions Fortnox { get; set; }

		public TokenOptions Token { get; set; }
	}
}

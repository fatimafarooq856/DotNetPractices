using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utils.Entities
{
	public abstract class RemovableEntity : IRemovableEntity
	{
		public Instant? Removed { get; set; }
		public string? RemovedBy { get; set; }
	}

	public interface IRemovableEntity
	{
		public Instant? Removed { get; set; }
		public string? RemovedBy { get; set; }
	}
}

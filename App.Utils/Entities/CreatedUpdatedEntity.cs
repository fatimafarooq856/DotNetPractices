using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utils.Entities
{
	public interface ICreatedUpdatedEntity : IUpdatedEntity
	{
		public Instant Created { get; set; }
		public string CreatedBy { get; set; }
	}

	public abstract class CreatedUpdatedEntity : UpdatedEntity, ICreatedUpdatedEntity
	{
		public Instant Created { get; set; }
		public string? CreatedBy { get; set; }
	}

	public interface IUpdatedEntity 
	{
		public Instant? Updated { get; set; }
		public string? UpdatedBy { get; set; }
	}

	public abstract class UpdatedEntity :  IUpdatedEntity
	{
		public Instant? Updated { get; set; }
		public string? UpdatedBy { get; set; }
	}
}

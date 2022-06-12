using App.Utils.Entities;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AppFile : CreatedUpdatedEntity, IRemovableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }      
        public Instant? Removed { get; set; }
        public string? RemovedBy { get; set; }
    }
}

using App.Utils.Entities;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Brand : CreatedUpdatedEntity, IRemovableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string NIC { get; set; }
        public LocalDate FromDate { get; set; }
        public LocalDate ToDate { get; set; }
        public Instant? Removed { get; set; }
        public string? RemovedBy { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
        public virtual ICollection<BrandFile> Files { get; set; }
    }
}

﻿using App.Utils.Entities;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Promotion : CreatedUpdatedEntity, IRemovableEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public string Name { get; set; }        
        public LocalDate FromDate { get; set; }
        public LocalDate ToDate { get; set; }
        public Instant? Removed { get; set; }
        public string? RemovedBy { get; set; }
        public virtual ICollection<PromotionFile> Files { get; set; }
    }
}

using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Class { get; set; }
        public string? NIC { get; set; }
        public LocalDate FromDate { get; set; }
        public LocalDate? ToDate { get; set; }
        public Instant? Created { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}

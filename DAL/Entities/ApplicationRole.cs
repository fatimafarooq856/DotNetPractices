using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsActive { get; set; } = true;
        public int StoreId { get; set; } = 0;
        public DateTime CreatedDate { get; set; }
        public bool IsSystem { set; get; }
        public bool IsDeleted { get; set; } = false;
        //public virtual ICollection<IdentityUserRole<string>> Users { get; set; }
    }
}

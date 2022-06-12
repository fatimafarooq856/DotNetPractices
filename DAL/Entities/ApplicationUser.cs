using Microsoft.AspNetCore.Identity;
using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("AspNetUsers")]
    public class User: IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPublic { get; set; }
        public Instant? Created { get; set; }
        public virtual ICollection<AppFile> Files { get; set; }
    }
}

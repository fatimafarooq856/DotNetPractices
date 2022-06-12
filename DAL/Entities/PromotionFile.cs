using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PromotionFile
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public virtual AppFile File { get; set; }
        public int PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}

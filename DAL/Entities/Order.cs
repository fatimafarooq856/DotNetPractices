using App.Utils.Entities;
using App.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Order : CreatedUpdatedEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public OrderType Type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Interfaces;

namespace AppCore.Entities
{
    public class Discount : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int ShelfItemId { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public virtual ShelfItem ShelfItem { get; set; }
    }
}

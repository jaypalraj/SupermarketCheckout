using AppCore.Interfaces;
using System;

namespace AppCore.Entities
{
    public class SpecialOffer : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int ShelfItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public virtual ShelfItem ShelfItem { get; set; }
    }
}
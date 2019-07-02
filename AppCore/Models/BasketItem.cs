using AppCore.Entities;

namespace AppCore.Models
{
    public class BasketItem
    {
        public int ShelfItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual ShelfItem ShelfItem { get; set; }
    }
}
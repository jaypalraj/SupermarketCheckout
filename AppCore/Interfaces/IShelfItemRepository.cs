using AppCore.Entities;
using System.Collections.Generic;

namespace AppCore.Interfaces
{
    public interface IShelfItemRepository
    {
        IEnumerable<ShelfItem> GetShelfItems();
    }
}
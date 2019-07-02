using AppCore.Entities;
using AppCore.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class ShelfItemRepository : IShelfItemRepository
    {
        private readonly IList<ShelfItem> _shelfItems;
        public ShelfItemRepository()
        {
            _shelfItems = ListShelfItems.FakeData();
        }

        public IEnumerable<ShelfItem> GetShelfItems()
        {
            return _shelfItems;
        }
    }
}
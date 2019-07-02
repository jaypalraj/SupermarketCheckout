using AppCore.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class ListShelfItems
    {
        public static List<ShelfItem> FakeData()
        {
            return new List<ShelfItem>
            {
                new ShelfItem()
                {
                    Id = 1,
                    Name = "Apple",
                    Price = 0.50m
                },
                new ShelfItem()
                {
                    Id = 2,
                    Name = "Biscuit",
                    Price = 0.30m
                },
                new ShelfItem()
                {
                    Id = 3,
                    Name = "Coffee",
                    Price = 1.80m
                },
                new ShelfItem()
                {
                    Id = 4,
                    Name = "Tissue",
                    Price = 0.99m
                }
            };
        }
    }
}

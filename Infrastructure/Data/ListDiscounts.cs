using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities;

namespace Infrastructure.Data
{
    public class ListDiscounts
    {
        public static List<Discount> FakeData()
        {
            return new List<Discount>
            {
                new Discount()
                {
                    Id = 1,
                    ShelfItemId = 1,
                    DiscountPercent = 50m,
                    ValidFrom = DateTime.Now.AddDays(-1),
                    ValidTo = DateTime.Now.AddDays(2)
                },
                new Discount()
                {
                    Id = 1,
                    ShelfItemId = 3,
                    DiscountPercent = 50m,
                    ValidFrom = DateTime.Now.AddDays(-1),
                    ValidTo = DateTime.Now.AddDays(2)
                },
                new Discount()
                {
                    Id = 2,
                    ShelfItemId = 4,
                    DiscountPercent = 25m,
                    ValidFrom = DateTime.Now.AddDays(-1),
                    ValidTo = DateTime.Now.AddDays(3)
                }

            };
        }
    }
}

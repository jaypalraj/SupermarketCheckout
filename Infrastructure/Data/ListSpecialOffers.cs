using AppCore.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class ListSpecialOffers
    {
        public static List<SpecialOffer> FakeData()
        {
            return new List<SpecialOffer>
            {
                new SpecialOffer()
                {
                    Id = 1,
                    ShelfItemId = 1,
                    Quantity = 3,
                    Price = 1.30m,
                    ValidFrom = DateTime.Now.AddDays(-1),
                    ValidTo = DateTime.Now.AddDays(2)
                },
                new SpecialOffer()
                {
                    Id = 2,
                    ShelfItemId = 2,
                    Quantity = 2,
                    Price = 0.45m,
                    ValidFrom = DateTime.Now.AddDays(-1),
                    ValidTo = DateTime.Now.AddDays(3)
                },
                new SpecialOffer()
                {
                    Id = 3,
                    ShelfItemId = 4,
                    Quantity = 1,
                    Price = 0.50m,
                    ValidFrom = DateTime.Now.AddMonths(1),
                    ValidTo = DateTime.Now.AddMonths(1).AddDays(7)
                }

            };
        }
    }
}

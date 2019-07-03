using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities;
using AppCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IList<Discount> _discounts;
        public DiscountRepository()
        {
            _discounts = ListDiscounts.FakeData();
        }

        public IEnumerable<Discount> GetActiveDiscounts()
        {
            return _discounts.Where(d => d.ValidFrom < DateTime.Now && d.ValidTo > DateTime.Now);
        }
    }
}

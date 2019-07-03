using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities;

namespace AppCore.Interfaces
{
    public interface IDiscountRepository
    {
        IEnumerable<Discount> GetActiveDiscounts();
    }
}

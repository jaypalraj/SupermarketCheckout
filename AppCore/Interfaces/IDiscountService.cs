using System.Collections.Generic;
using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IDiscountService
    {
        void ApplyDiscountToBasketItemPrices(IList<BasketItem> basketItems);
    }
}
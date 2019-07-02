using AppCore.Models;
using System.Collections.Generic;

namespace AppCore.Interfaces
{
    public interface ISpecialOfferService
    {
        void ApplySpecialOffersToBasketItemPrices(IList<BasketItem> basketItems);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public void ApplyDiscountToBasketItemPrices(IList<BasketItem> basketItems)
        {
            var discounts = _discountRepository.GetActiveDiscounts()?.ToArray();

            foreach (var basketItem in basketItems)
            {
                var discount = discounts?.SingleOrDefault(d => d.ShelfItemId == basketItem.ShelfItemId);

                if (discount != null)
                {
                    basketItem.Price = Math.Round(basketItem.Price - basketItem.Price * (discount.DiscountPercent / 100),2);
                }
            }
        }
    }
}

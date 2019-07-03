using AppCore.Entities;
using AppCore.Interfaces;
using AppCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly ISpecialOfferService _specialOfferService;
        private static IList<BasketItem> _basketItems;

        public BasketService(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
            _basketItems = new List<BasketItem>();
        }


        public int GetBasketItemsCount()
        {
            return _basketItems?.Sum(i => i.Quantity) ?? 0;
        }

        public void AddItemToBasket(ShelfItem item, int quantity = 1)
        {
            var basketItem = _basketItems.SingleOrDefault(b => b.ShelfItemId == item.Id);

            if (basketItem == null)
            {
                basketItem = new BasketItem()
                {
                    ShelfItemId = item.Id,
                    Price = item.Price,
                    Quantity = quantity,
                    ShelfItem = item
                };

                _basketItems.Add(basketItem);
            }
            else
            {
                basketItem.Quantity += quantity;
            }
        }

        public decimal CalculateBasketTotal()
        {
            _specialOfferService.ApplySpecialOffersToBasketItemPrices(_basketItems);

            return _basketItems.Sum(i => i.Price);
        }

        public IList<BasketItem> GetBasketItems()
        {
            return _basketItems;
        }



        public void Dispose()
        {
            _basketItems.Clear();
        }
    }
}
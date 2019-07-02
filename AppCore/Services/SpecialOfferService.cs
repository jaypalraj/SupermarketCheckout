using AppCore.Entities;
using AppCore.Interfaces;
using AppCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppCore.Services
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly ISpecialOfferRepository _specialOfferRepository;
        public SpecialOfferService(ISpecialOfferRepository specialOfferRepository)
        {
            _specialOfferRepository = specialOfferRepository;
        }

        private static void CalculateTotalWithSpecialOffer(BasketItem basketItem, SpecialOffer itemSpecialOffer, ref int basketItemQty, ref decimal total)
        {
            while (true)
            {
                if (basketItemQty == itemSpecialOffer.Quantity)
                {
                    total += itemSpecialOffer.Price;
                }
                else
                {
                    if (basketItemQty > itemSpecialOffer.Quantity)
                    {
                        basketItemQty -= itemSpecialOffer.Quantity;

                        total += itemSpecialOffer.Price;
                        continue;
                    }
                    else
                    {
                        total += basketItem.Price * basketItemQty;
                    }
                }

                break;
            }
        }


        public void ApplySpecialOffersToBasketItemPrices(IList<BasketItem> basketItems)
        {
            var specialOffers = _specialOfferRepository.GetActiveSpecialOffers().ToArray();

            foreach (var basketItem in basketItems)
            {
                var itemSpecialOffer = specialOffers.SingleOrDefault(s => s.ShelfItemId == basketItem.ShelfItemId);

                if (itemSpecialOffer != null)
                {
                    var total = 0m;
                    var basketItemQty = basketItem.Quantity;

                    CalculateTotalWithSpecialOffer(basketItem, itemSpecialOffer, ref basketItemQty, ref total);

                    basketItem.Price = total;
                }
                else
                {
                    basketItem.Price = basketItem.Price * basketItem.Quantity;
                }
            }

        }
    }
}
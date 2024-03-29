﻿using AppCore.Entities;
using AppCore.Interfaces;
using AppCore.Services;
using AppCore.Utilities;
using Infrastructure.Logging;
using Infrastructure.Repositories;
using System;
using System.Linq;
using static System.Console;


namespace CheckoutClient
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var shelfItemRepository = new ShelfItemRepository();
                var shelfItems = shelfItemRepository.GetShelfItems().ToArray();

                var discountRepository = new DiscountRepository();
                var discounts = discountRepository.GetActiveDiscounts().ToArray();

                var specialOfferRepository = new SpecialOfferRepository();
                var specialOffers = specialOfferRepository.GetActiveSpecialOffers().ToArray();


                DisplayShelfItems(shelfItems, discounts, specialOffers);


                IDiscountService discountService = new DiscountService(discountRepository);
                ISpecialOfferService specialOfferService = new SpecialOfferService(specialOfferRepository);
                using (var basketService = new BasketService(discountService, specialOfferService))
                {
                    BasketHandler(shelfItems, basketService);

                    PrintCustomerReceipt(specialOffers, discounts, basketService);
                }

                ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine("\n\n");

                ILogger logger = new ConsoleLogger();
                logger.LogException(ex);

                ReadLine();
            }
        }



        private static Tuple<Discount, SpecialOffer> GetApplicableDiscountAndSpecialOfferOnShelfItem(int shelfItemId,
            Discount[] discounts, SpecialOffer[] specialOffers)
        {
            var discount = discounts.SingleOrDefault(d => d.ShelfItemId == shelfItemId);

            var specialOffer = specialOffers.SingleOrDefault(s => s.ShelfItemId == shelfItemId);

            return new Tuple<Discount, SpecialOffer>(discount, specialOffer);
        }

        private static void DisplayShelfItems(ShelfItem[] shelfItems, Discount[] discounts, SpecialOffer[] specialOffers)
        {
            WriteLine("\n\t ITEMS ON DISPLAY SHELF...\n");
            Write("\t =============================================== \n\n");
            foreach (var item in shelfItems)
            {
                var discountAndSpecialOffer = GetApplicableDiscountAndSpecialOfferOnShelfItem(item.Id, discounts, specialOffers);
                var discount = discountAndSpecialOffer.Item1;
                var specialOffer = discountAndSpecialOffer.Item2;

                var shelfItemDisplay = $"\t{item.Id} - {item.Name} ------------------ {item.Price:C2}";

                if (discount != null)
                    shelfItemDisplay += $" @ ({discount.DiscountPercent}% discount per unit) ";

                if (specialOffer != null)
                    shelfItemDisplay += $" ({specialOffer.Quantity} for {specialOffer.Price:C2})";

                WriteLine(shelfItemDisplay);
            }
            Write("\n\t =============================================== \n");
            Write("\t *Please note that discounts are not applied when special offers are applicable \n\n");
        }

        private static void BasketHandler(ShelfItem[] shelfItems, IBasketService basketService)
        {
            while (true)
            {
                Write("\n ENTER ITEM NUMBER TO ADD IT TO THE BASKET (ENTER 'N' TO CHECKOUT): ");
                var userInput = ReadKey();

                if (!Helper.ValidUserInput(userInput,"PLEASE ENTER ONE OF THE NUMBERS FROM THE ABOVE LIST"))
                    break;

                var shelfItemId = int.Parse(userInput.KeyChar.ToString());

                if (shelfItems.All(s => s.Id != shelfItemId))
                {
                    WriteLine("\n PLEASE ENTER ONE OF THE NUMBERS FROM THE ABOVE LIST \n");
                    continue;
                }

                var selectedShelfItem = shelfItems.SingleOrDefault(s => s.Id == shelfItemId);

                if (selectedShelfItem == null) continue;

                Write($"\n ENTER QTY OF {selectedShelfItem.Name.ToUpper()}: ");
                var qtyUserInput = ReadLine();

                if (!Helper.ValidUserInput(qtyUserInput, "QUANTITY MUST BE A WHOLE NUMBER"))
                    continue;
                
                int.TryParse(qtyUserInput, out var quantity);

                basketService.AddItemToBasket(selectedShelfItem, quantity);
            }
        }

        private static void PrintCustomerReceipt(SpecialOffer[] specialOffers, Discount[] discounts, IBasketService basketService)
        {
            var totalBill = basketService.CalculateBasketTotal();


            Write("\n\n\n");
            WriteLine("\t CUSTOMER RECEIPT");
            Write("\t ==============================================");

            foreach (var basketItem in basketService.GetBasketItems())
            {
                var discountAndSpecialOffer = GetApplicableDiscountAndSpecialOfferOnShelfItem(basketItem.ShelfItem.Id, discounts, specialOffers);
                var discount = discountAndSpecialOffer.Item1;
                var specialOffer = discountAndSpecialOffer.Item2;

                var receiptItemDisplay = $"\n\t {basketItem.ShelfItem.Name} - {basketItem.ShelfItem.Price:C2} ";

                if (discount != null)
                    receiptItemDisplay += $"@ ({discount.DiscountPercent}%) ";

                receiptItemDisplay += $"* {basketItem.Quantity} => {basketItem.Price:C2}";

                if (specialOffer != null)
                    receiptItemDisplay += $" ({specialOffer.Quantity} for {specialOffer.Price:C2})";

                WriteLine(receiptItemDisplay);
            }

            Write("\t ==============================================");
            WriteLine($"\n\t YOUR TOTAL BILL IS: {totalBill:C2}");
            Write("\t ==============================================");
        }
    }
}

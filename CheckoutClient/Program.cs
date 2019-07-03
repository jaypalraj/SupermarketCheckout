using AppCore.Entities;
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

                var specialOfferRepository = new SpecialOfferRepository();
                var specialOffers = specialOfferRepository.GetActiveSpecialOffers().ToArray();


                DisplayShelfItems(shelfItems, specialOffers);


                using (var basketService = new BasketService(new SpecialOfferService(specialOfferRepository)))
                {
                    BasketHandler(shelfItems, basketService);

                    PrintCustomerReceipt(specialOffers, basketService);
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





        private static void DisplayShelfItems(ShelfItem[] shelfItems, SpecialOffer[] specialOffers)
        {
            WriteLine("\n\t ITEMS ON DISPLAY SHELF...\n");
            Write("\t =============================================== \n\n");
            foreach (var item in shelfItems)
            {
                var specialOffer = specialOffers.SingleOrDefault(s => s.ShelfItemId == item.Id);

                var shelfItemDisplay = $"\t{item.Id} - {item.Name} ------------------ {item.Price:C2}";

                if (specialOffer != null)
                    shelfItemDisplay = shelfItemDisplay + $" ({specialOffer.Quantity} for {specialOffer.Price:C2})";

                WriteLine(shelfItemDisplay);
            }
            Write("\n\t =============================================== \n\n");
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

        private static void PrintCustomerReceipt(SpecialOffer[] specialOffers, IBasketService basketService)
        {
            var totalBill = basketService.CalculateBasketTotal();


            Write("\n\n\n");
            WriteLine("\t CUSTOMER RECEIPT");
            Write("\t ==============================================");

            foreach (var basketItem in basketService.GetBasketItems())
            {
                var specialOffer = specialOffers.SingleOrDefault(s => s.ShelfItemId == basketItem.ShelfItem.Id);
                var receiptItemDisplay = $"\n\t {basketItem.ShelfItem.Name} - {basketItem.ShelfItem.Price:C2} * {basketItem.Quantity} => {basketItem.Price:C2}";
                if (specialOffer != null)
                    receiptItemDisplay = receiptItemDisplay + $" ({specialOffer.Quantity} for {specialOffer.Price:C2})";
                WriteLine(receiptItemDisplay);
            }

            Write("\t ==============================================");
            WriteLine($"\n\t YOUR TOTAL BILL IS: {totalBill:C2}");
            Write("\t ==============================================");
        }
    }
}

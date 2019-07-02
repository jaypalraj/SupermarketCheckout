using System.Linq;
using AppCore.Interfaces;
using AppCore.Services;
using Infrastructure.Repositories;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        private IShelfItemRepository _shelfItemRepository;
        private ISpecialOfferService _specialOfferService;
        private IBasketService _basketService;

        [SetUp]
        public void Setup()
        {

            var specialOfferRepository = new SpecialOfferRepository();
            _specialOfferService = new SpecialOfferService(specialOfferRepository);

            _shelfItemRepository = new ShelfItemRepository();

            _basketService = new BasketService(_specialOfferService);

        }

        [TearDown]
        public void Teardown()
        {
            _basketService.Dispose();
        }


        [Test]
        public void AddItemsToBasket()
        {
            var shelfItems = _shelfItemRepository.GetShelfItems().ToArray();


            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);

            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]);

            _basketService.AddItemToBasket(shelfItems[2]);

            _basketService.AddItemToBasket(shelfItems[3]);

            Assert.AreEqual(7, _basketService.GetBasketItemsCount());
        }


        [Test]
        public void AddCorrectQuantityOfItemsToTheBasket()
        {
            var shelfItems = _shelfItemRepository.GetShelfItems().ToArray();


            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);

            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1],2);

            _basketService.AddItemToBasket(shelfItems[2]);

            _basketService.AddItemToBasket(shelfItems[3],4);

            Assert.AreEqual(11, _basketService.GetBasketItemsCount());

        }

        [Test]
        public void CalculateBasketTotalWhenQuantityIsExplicitlySpecified()
        {
            var shelfItems = _shelfItemRepository.GetShelfItems().ToArray();

            _basketService.AddItemToBasket(shelfItems[0],3);

            _basketService.AddItemToBasket(shelfItems[1], 2);

            _basketService.AddItemToBasket(shelfItems[2]);

            _basketService.AddItemToBasket(shelfItems[3]);

            Assert.AreEqual(4.54m, _basketService.CalculateBasketTotal());
        }


        [Test]
        public void CalculateBasketTotalWhenQuantityMatchesWithSpecialOffersQuantity()
        {
            var shelfItems = _shelfItemRepository.GetShelfItems().ToArray();

            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);

            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]);

            _basketService.AddItemToBasket(shelfItems[2]);

            _basketService.AddItemToBasket(shelfItems[3]);

            Assert.AreEqual(4.54m, _basketService.CalculateBasketTotal());
        }


        [Test]
        public void CalculateBasketTotalWhenQuantityDoesNotMatchWithSpecialOffersQuantity()
        {
            var shelfItems = _shelfItemRepository.GetShelfItems().ToArray();

            _basketService.AddItemToBasket(shelfItems[0]); //1.30
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]); //0.50
            _basketService.AddItemToBasket(shelfItems[0]); //0.50

            _basketService.AddItemToBasket(shelfItems[1]); //0.45
            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]); //0.45
            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]); //0.30

            _basketService.AddItemToBasket(shelfItems[2]); //1.80

            _basketService.AddItemToBasket(shelfItems[3]); //0.99

            Assert.AreEqual(6.29m, _basketService.CalculateBasketTotal());
        }


        [Test]
        public void CalculateBasketTotalWhenQuantityDoesNotMatchWithSpecialOffersQuantityInDifferentSequence()
        {
            var shelfItems = _shelfItemRepository.GetShelfItems().ToArray();

            _basketService.AddItemToBasket(shelfItems[3]);

            _basketService.AddItemToBasket(shelfItems[0]); 
            _basketService.AddItemToBasket(shelfItems[0]);

            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]);

            _basketService.AddItemToBasket(shelfItems[0]);
             
            _basketService.AddItemToBasket(shelfItems[0]); 

            _basketService.AddItemToBasket(shelfItems[1]);

            _basketService.AddItemToBasket(shelfItems[0]);

            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]); 

            _basketService.AddItemToBasket(shelfItems[2]); 


            Assert.AreEqual(6.29m, _basketService.CalculateBasketTotal());
        }




        [Test]
        public void CalculateBasketTotalWhenQuantityDoesNotMatchWithSpecialOffersQuantity_scenario1()
        {
            var shelfItems = _shelfItemRepository.GetShelfItems().ToArray();

            _basketService.AddItemToBasket(shelfItems[0]); //1.30
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]); //1.30
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]); //0.50
            _basketService.AddItemToBasket(shelfItems[0]); //0.50

            _basketService.AddItemToBasket(shelfItems[1]); //0.45
            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]); //0.45
            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]); //0.30

            _basketService.AddItemToBasket(shelfItems[2]); //1.80

            _basketService.AddItemToBasket(shelfItems[3]); //0.99

            Assert.AreEqual(7.59m, _basketService.CalculateBasketTotal());
        }


        [Test]
        public void CalculateBasketTotalWhenQuantityDoesNotMatchWithSpecialOffersQuantity_scenario2()
        {
            var shelfItems = _shelfItemRepository.GetShelfItems().ToArray();

            _basketService.AddItemToBasket(shelfItems[0]); //1.30
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]); //1.30
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]);
            _basketService.AddItemToBasket(shelfItems[0]); //0.50
            _basketService.AddItemToBasket(shelfItems[0]); //0.50

            _basketService.AddItemToBasket(shelfItems[1]); //0.45
            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]); //0.45
            _basketService.AddItemToBasket(shelfItems[1]);
            _basketService.AddItemToBasket(shelfItems[1]); //0.30

            _basketService.AddItemToBasket(shelfItems[2]); //1.80
            _basketService.AddItemToBasket(shelfItems[2]); //1.80

            _basketService.AddItemToBasket(shelfItems[3]); //0.99

            Assert.AreEqual(9.39m, _basketService.CalculateBasketTotal());
        }

    }
}
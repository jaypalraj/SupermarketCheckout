using System;
using System.Collections;
using System.Collections.Generic;
using AppCore.Entities;
using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IBasketService : IDisposable
    {
        int GetBasketItemsCount();
        void AddItemToBasket(ShelfItem item, int quantity = 1);
        decimal CalculateBasketTotal();
        IList<BasketItem> GetBasketItems();
    }
}
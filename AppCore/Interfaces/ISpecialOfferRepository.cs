using AppCore.Entities;
using System.Collections.Generic;

namespace AppCore.Interfaces
{
    public interface ISpecialOfferRepository
    {
        IEnumerable<SpecialOffer> GetActiveSpecialOffers();
        SpecialOffer GetSpecialOfferForShelfItem(int shelfItemId);
    }
}
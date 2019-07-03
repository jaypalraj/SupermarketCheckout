using AppCore.Entities;
using AppCore.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class SpecialOfferRepository : ISpecialOfferRepository
    {
        private readonly IList<SpecialOffer> _specialOffers;
        public SpecialOfferRepository()
        {
            _specialOffers = ListSpecialOffers.FakeData();
        }

        public IEnumerable<SpecialOffer> GetActiveSpecialOffers()
        {
            return _specialOffers.Where(s => s.ValidFrom < DateTime.Now && s.ValidTo > DateTime.Now);
        }
    }
}
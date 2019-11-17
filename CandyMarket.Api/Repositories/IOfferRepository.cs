using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.Repositories
{
    public interface IOfferRepository
    {
        IEnumerable<Offer> GetOffers();
        bool AddOffer(AddOfferDto newOffer);
        bool RemoveOffer(Guid userCandyId);
        bool DeleteOffer(Guid offerId);
    }
}

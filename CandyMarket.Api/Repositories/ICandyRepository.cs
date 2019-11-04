using System;
using System.Collections.Generic;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;

namespace CandyMarket.Api.Repositories
{
    public interface ICandyRepository
    {
        IEnumerable<Candy> GetAllCandy();
        Candy GetCandyById(Guid candyId);
        User GetUserById(Guid userId);
        Guid GetCandyIdFromDatabase(Guid userCandyId);

        Guid GetUserIdFromDatabase(Guid userCandyId);
        bool AddCandy(AddCandyDto newCandy);
        bool EatCandy(Guid candyIdToDelete, Guid userIdWhoIsEating);
        User FavoriteCandy(Guid candyId);
        bool DonateCandy(Guid candyIdToDonate, Guid userIdWhoIsDonating);
    }
}
using System;
using System.Collections.Generic;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;

namespace CandyMarket.Api.Repositories
{
    public interface IUserRepository
    {
        User GetUser(Guid userId);
        bool AddUser(AddUserDto newUser);
        Guid GetUserIdFromDatabase(Guid userCandyId);
        Guid GetCandyIdFromDatabase(Guid userCandyId);
        bool BuyCandy(Guid userIdWhoIsBuying, Guid candyIdGettingBought);
        bool EatCandy(Guid candyIdToDelete, Guid userIdWhoIsEating);
        User FavoriteCandy(Guid candyId);
        bool DonateCandy(Guid candyIdToDonate, Guid userIdWhoIsDonating);

        User GetUserById(Guid userId);
    }
}

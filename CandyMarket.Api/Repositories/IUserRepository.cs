using System;
using System.Collections.Generic;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;

namespace CandyMarket.Api.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(Guid userId);
        bool AddUser(AddUserDto newUser);
        Guid GetUserIdFromDatabase(Guid userCandyId);
        Guid GetCandyIdFromDatabase(Guid userCandyId);
        bool BuyCandy(Guid userIdWhoIsBuying, Guid candyIdGettingBought);
        bool DeleteUserCandyEntry(Guid userCandyIdToDelete);
        bool EatCandy(Guid candyIdToDelete);
        User WhoToDonateTo(Guid candyId, Guid userIdWhoIsDonating);
        bool DonateCandy(Guid userCandyIdToDelete);

        User GetUserById(Guid userId);
    }
}

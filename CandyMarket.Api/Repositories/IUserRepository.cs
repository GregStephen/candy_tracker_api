using System;
using System.Collections.Generic;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;

namespace CandyMarket.Api.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<Trade> GetAllTrades();
        User GetUser(Guid userId);
        User GetUserByEmailAndPassword(string email, string password);
        bool AddUser(AddUserDto newUser);
        User GetUserFromDatabase(Guid userCandyId);
        bool BuyCandy(Guid userIdWhoIsBuying, Guid candyIdGettingBought);
        bool DeleteUserCandyEntry(Guid userCandyIdToDelete);
        bool EatCandy(Guid userIdWhoIsEating, Guid candyIdToDelete);
        User WhoToDonateTo(Guid candyId, Guid userIdWhoIsDonating);
        bool DonateCandy(Guid userIdWhoIsDonating, Guid candyIdToDonate);
        User GetUserById(Guid userId);
        bool PutCandyUpForTrade(Guid userCandyId);
        bool TakeCandyOffTrade(Guid userCandyId);
        bool TradeCandy(Guid userCandyId1, Guid userCandyId2);
    }
}

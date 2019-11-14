using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;
using Dapper;

namespace CandyMarket.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        string _connectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";

       

        public IEnumerable<User> GetAllUsers()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var candyRepo = new CandyRepository();
                var users = db.Query<User>
                   (
                   @"SELECT * 
                      FROM [User]"
                   ).ToList();
                foreach (User user in users)
                {
                    var candies = candyRepo.FetchUsersCandyList(user);
                    user.CandyOwned = candies;
                    var favoriteCandyName = candyRepo.FetchFavoriteCandyName(user);
                    user.FavoriteTypeOfCandyName = favoriteCandyName;
                }
                return users;
            }
        }

        public IEnumerable<Trade> GetAllTrades()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT uc.Id as UserCandyId, 
                                    uc.IsUpForTrade, 
                                    uc.CandyId, 
                                    uc.UserId,
                                    u.FirstName,
                                    u.LastName,
                                    c.Name as CandyName,
                                    c.ImgUrl,
                                    c.Size,
                                    ct.Name as Type
                            FROM [UserCandy] uc
                                JOIN [User] u
                                ON uc.UserId = u.Id
                                JOIN [Candy] c
                                On uc.CandyId = c.Id
                                JOIN [Type] ct
                                ON c.TypeId = ct.Id
                            WHERE uc.[IsUpForTrade] = 1";
                var trades = db.Query<Trade>(sql);
                return trades;
                
            }
        }
        public User GetUser(Guid userId)
        {
            var candyRepo = new CandyRepository();
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT *
                            FROM [User]
                            WHERE [Id] = @userId";
                var parameters = new { userId };
                var user = db.QueryFirst<User>(sql, parameters);
                var candies = candyRepo.FetchUsersCandyList(user);
                user.CandyOwned = candies;
                var favoriteCandyName = candyRepo.FetchFavoriteCandyName(user);
                user.FavoriteTypeOfCandyName = favoriteCandyName;
                return user;
            }
        }
        public bool AddUser(AddUserDto newUser)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO [User]
                        ([FirstName]
                        ,[LastName]
                        ,[Email]
                        ,[Password]
                        ,[FavoriteTypeOfCandyId])
                    VALUES
                        (@FirstName
                        ,@LastName
                        ,@Email
                        ,@Password
                        ,@FavoriteTypeOfCandyId)";
                return db.Execute(sql, newUser) == 1;
            }
        }
        public User GetUserFromDatabase(Guid userCandyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT u.*
                            FROM [UserCandy] uc
                                JOIN [User] u
                                ON u.[Id] = uc.[Id]
                            WHERE uc.[Id] = @userCandyId";
                var parameters = new { userCandyId };
                var userIdToReturn = db.QueryFirst<User>(sql, parameters);
                return userIdToReturn;
            }
        }
      
        public User GetUserById(Guid userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT *
                            FROM [User]
                            WHERE [Id] = @userId";
                var parameters = new { userId };
                var userToReturn = db.QueryFirst<User>(sql, parameters);
                return userToReturn;
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var candyRepo = new CandyRepository();
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT *
                            FROM [User]
                            WHERE ([Password] = @password AND [Email] = @email)";
                var parameters = new { email, password };
                var userToReturn = db.QueryFirst<User>(sql, parameters);
                var candies = candyRepo.FetchUsersCandyList(userToReturn);
                userToReturn.CandyOwned = candies;
                var favoriteCandyName = candyRepo.FetchFavoriteCandyName(userToReturn);
                userToReturn.FavoriteTypeOfCandyName = favoriteCandyName;
                return userToReturn;
            }
        }
        public bool BuyCandy(Guid userIdWhoIsBuying, Guid candyIdGettingBought)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [UserCandy]
                                ([UserId], [CandyId])
                            VALUES
                                (@userId, @candyId)";
                var parameters = new { userId = userIdWhoIsBuying, candyId = candyIdGettingBought };
                return db.Execute(sql, parameters) == 1;
            }
        }
        public bool DeleteUserCandyEntry(Guid userCandyIdToDelete)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE
                            FROM UserCandy
                            WHERE Id = @UserCandyId";
                return db.Execute(sql, new { userCandyId = userCandyIdToDelete }) == 1;
            }
        }
        public Guid FindUserCandyId(Guid userIdWhoIsEating, Guid candyIdToDelete)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT Id
                            FROM [UserCandy]
                            WHERE ([UserId] = @userId AND [CandyId] = @candyId)";
                var parameters = new { userId = userIdWhoIsEating, candyId = candyIdToDelete };
                return db.QueryFirst<Guid>(sql, parameters);
            }
        }
        public bool EatCandy(Guid userIdWhoIsEating, Guid userCandyIdToDelete)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                DeleteUserCandyEntry(userCandyIdToDelete);
                var sql = @"UPDATE [User]
                            SET AmountOfCandyEaten += 1
                            WHERE Id = @UserId";
                return db.Execute(sql, new { UserId = userIdWhoIsEating }) == 1;
            }
        }
        public User WhoToDonateTo(Guid candyId, Guid userIdWhoIsDonating)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var candyRepo = new CandyRepository();
                var candyTypeId = candyRepo.GetCandyTypeId(candyId);
                var sql = @"SELECT * 
                            FROM [User]
                            WHERE ([FavoriteTypeOfCandyId] = @candyTypeId AND [Id] != @userId)";
                // in the future add a bit that makes it whomever has the least amount of candy as well
                var parameters = new { CandyTypeId = candyTypeId, UserId = userIdWhoIsDonating };
                var userToDonateTo = db.QueryFirstOrDefault<User>(sql, parameters);
                /* If their is no user who has that for their favorite candy
                    Then it looks for the user with the least amount of candy */
                if (userToDonateTo == null)
                {
                    var users = GetAllUsers().ToList();
                    var user = users.OrderBy(user => user.CandyOwned.Count()).First<User>();
                    return user;
                }
                return userToDonateTo;
            }

        }
        public bool DonateCandy(Guid userIdWhoIsDonating, Guid userCandyIdToDonate)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var candyRepo = new CandyRepository();
                var candyToDonate = candyRepo.GetCandyFromDatabase(userCandyIdToDonate);
                var userToDonate = WhoToDonateTo(candyToDonate.Id, userIdWhoIsDonating);
                DeleteUserCandyEntry(userCandyIdToDonate);
                var sql = @"INSERT INTO [UserCandy]
                            ([UserId], [CandyId])
                            VALUES
                                (@userId, @candyId)";
                var sql2 = @"UPDATE [User]
                            SET AmountOfCandyDonated += 1
                            WHERE Id = @UserId";
                db.Execute(sql2, new { UserId = userIdWhoIsDonating });
                return db.Execute(sql, new { CandyId = candyToDonate.Id, UserId = userToDonate.Id }) == 1;
            }
        }

        public bool PutCandyUpForTrade(Guid userCandyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [UserCandy]
                            SET IsUpForTrade = 1
                            WHERE [Id] = @userCandyId";
                var parameters = new { userCandyId };
                return db.Execute(sql, parameters) == 1;
            }
        }
        public bool TakeCandyOffTrade(Guid userCandyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [UserCandy]
                            SET IsUpForTrade = 0
                            WHERE [Id] = @userCandyId";
                var parameters = new { userCandyId };
                return db.Execute(sql, parameters) == 1;
            }
        }

        public bool TradeCandy(Guid userCandyId1, Guid userCandyId2)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var candyRepo = new CandyRepository();
                var userId1 = GetUserFromDatabase(userCandyId1);
                var userId2 = GetUserFromDatabase(userCandyId2);
                var candyId1 = candyRepo.GetCandyFromDatabase(userCandyId1);
                var candyId2 = candyRepo.GetCandyFromDatabase(userCandyId2);
                DeleteUserCandyEntry(userCandyId1);
                DeleteUserCandyEntry(userCandyId2);
                var sql = @"INSERT INTO [UserCandy]
                            ([Userid], [CandyId])
                            VALUES
                                (@userId1, @candyId2),
                                (@userId2, @candyId1)";
                var parameters = new { userId1, userId2, candyId1, candyId2 };
                return db.Execute(sql, parameters) == 1;
            }
        }
    }
}

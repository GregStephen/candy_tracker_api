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
                var users = db.Query<User>
                   (
                   @"SELECT * 
                      FROM [User]"
                   ).ToList();
                foreach (User user in users)
                {
                    var sql = @"SELECT c.Id, c.Name, c.Price, c.TypeId, c.Size
                             FROM [UserCandy] uc
                                JOIN [Candy] c ON uc.CandyId = c.Id
                             WHERE UserId = @userId";
                    var parameters = new { userId = user.Id };
                    var candies = db.Query<Candy>(sql, parameters);
                    user.CandyOwned = candies.ToList();
                    var sql3 = @"SELECT Name
                             FROM [Type]
                             WHERE Id = @candyId";
                    var parameters3 = new { candyId = user.FavoriteTypeOfCandyId };
                    var favoriteCandyName = db.QueryFirst<string>(sql3, parameters3);
                    user.FavoriteTypeOfCandyName = favoriteCandyName;
                }
                return users;
            }
        }

        public User GetUser(Guid userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT *
                            FROM [User]
                            WHERE [Id] = @userId";
                var parameters = new { userId };
                var user = db.QueryFirst<User>(sql, parameters);
                var sql2 = @"SELECT *
                             FROM [UserCandy]
                             WHERE UserId = @userId";
                var candies = db.Query<Candy>(sql2, parameters);
                user.CandyOwned = candies.ToList();
                var sql3 = @"SELECT Name
                             FROM [Type]
                             WHERE Id = @candyId";
                var parameters3 = new { candyId = user.FavoriteTypeOfCandyId };
                var favoriteCandyName = db.QueryFirst<string>(sql3, parameters3);
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
                        ,[FavoriteTypeOfCandyId])
                    VALUES
                        (@FirstName
                        ,@LastName
                        ,@FavoriteTypeOfCandyId)";
                return db.Execute(sql, newUser) == 1;
            }
        }
        public Guid GetUserIdFromDatabase(Guid userCandyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT UserId
                            FROM [UserCandy]
                            WHERE [Id] = @userCandyId";
                var parameters = new { userCandyId };
                var userIdToReturn = db.QueryFirst<Guid>(sql, parameters);
                return userIdToReturn;
            }
        }
        public Guid GetCandyIdFromDatabase(Guid userCandyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT CandyId
                            FROM [UserCandy]
                            WHERE [Id] = @userCandyId";
                var parameters = new { userCandyId };
                var candyIdToReturn = db.QueryFirst<Guid>(sql, parameters);
                return candyIdToReturn;
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
        public bool EatCandy(Guid userCandyIdToDelete)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var userIdWhoIsEating = GetUserIdFromDatabase(userCandyIdToDelete);
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
                var sql = @"SELECT TypeId
                             FROM [Candy]
                             WHERE [Id] = @candyId";
                var candyTypeId = db.QueryFirst<int>(sql, new { CandyId = candyId });
                var sql2 = @"SELECT * 
                            FROM [User]
                            WHERE ([FavoriteTypeOfCandyId] = @candyTypeId AND [Id] != @userId)";
                var parameters = new { CandyTypeId = candyTypeId, UserId = userIdWhoIsDonating };
                var userToDonateTo = db.QueryFirstOrDefault<User>(sql2, parameters);
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
        public bool DonateCandy(Guid userCandyIdToDonate)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var candyIdToDonate = GetCandyIdFromDatabase(userCandyIdToDonate);
                var userIdWhoIsDonating = GetUserIdFromDatabase(userCandyIdToDonate);
                var userToDonate = WhoToDonateTo(candyIdToDonate, userIdWhoIsDonating);
                DeleteUserCandyEntry(userCandyIdToDonate);
                var sql = @"INSERT INTO [UserCandy]
                            ([UserId], [CandyId])
                            VALUES
                                (@userId, @candyId)";
                var sql2 = @"UPDATE [User]
                            SET AmountOfCandyDonated += 1
                            WHERE Id = @UserId";
                db.Execute(sql2, new { UserId = userIdWhoIsDonating });
                return db.Execute(sql, new { CandyId = candyIdToDonate, UserId = userToDonate.Id }) == 1;
            }
        }
    }
}

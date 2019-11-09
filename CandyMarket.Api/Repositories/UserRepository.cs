﻿using System;
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

        public string FetchFavoriteCandyName(User user)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT Name
                             FROM [Type]
                             WHERE Id = @candyId";
                var parameters = new { candyId = user.FavoriteTypeOfCandyId };
                var favoriteCandyName = db.QueryFirst<string>(sql, parameters);
                return favoriteCandyName;
            }
        }
        public List<Candy> FetchUsersCandyList(User user)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT c.Id, c.Name, c.ImgUrl, c.TypeId, c.Size
                             FROM [UserCandy] uc
                                JOIN [Candy] c ON uc.CandyId = c.Id
                             WHERE UserId = @userId";
                var parameters = new { userId = user.Id };
                var candies = db.Query<Candy>(sql, parameters).ToList();
                return candies;
            }
        }
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
                    var candies = FetchUsersCandyList(user);
                    user.CandyOwned = candies;
                    var favoriteCandyName = FetchFavoriteCandyName(user);
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
                var candies = FetchUsersCandyList(user);
                user.CandyOwned = candies;
                var favoriteCandyName = FetchFavoriteCandyName(user);
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

        public User GetUserByEmailAndPassword(string email, string password)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT *
                            FROM [User]
                            WHERE ([Password] = @password AND [Email] = @email)";
                var parameters = new { email, password };
                var userToReturn = db.QueryFirst<User>(sql, parameters);
                var candies = FetchUsersCandyList(userToReturn);
                userToReturn.CandyOwned = candies;
                var favoriteCandyName = FetchFavoriteCandyName(userToReturn);
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
        public Guid findUserCandyId(Guid userIdWhoIsEating, Guid candyIdToDelete)
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
        public bool EatCandy(Guid userIdWhoIsEating, Guid candyIdToDelete)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var userCandyIdToDelete = findUserCandyId(userIdWhoIsEating, candyIdToDelete);
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
        public bool DonateCandy(Guid userIdWhoIsDonating, Guid candyIdToDonate)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var userToDonate = WhoToDonateTo(candyIdToDonate, userIdWhoIsDonating);
                var userCandyIdToDonate = findUserCandyId(userIdWhoIsDonating, candyIdToDonate);
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
        public bool TradeCandy(Guid userCandyId1, Guid userCandyId2)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var userId1 = GetUserIdFromDatabase(userCandyId1);
                var candyId1 = GetCandyIdFromDatabase(userCandyId1);
                var userId2 = GetUserIdFromDatabase(userCandyId2);
                var candyId2 = GetCandyIdFromDatabase(userCandyId2);
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

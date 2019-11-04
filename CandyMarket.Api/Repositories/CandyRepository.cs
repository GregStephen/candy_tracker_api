﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;

namespace CandyMarket.Api.Repositories
{
    public class CandyRepository : ICandyRepository
    {
        string _connectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";
        public IEnumerable<Candy> GetAllCandy()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var candies = db.Query<Candy>
                   (
                   @"SELECT * 
                      FROM Candy"
                   );
                return candies;
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
        public Candy GetCandyById(Guid candyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT *
                            FROM [Candy]
                            WHERE [Id] = @candyId";
                var parameters = new { candyId };
                var candyToReturn = db.QueryFirst<Candy>(sql, parameters);
                return candyToReturn;
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
        public bool AddCandy(AddCandyDto newCandy)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO [Candy]
                        ([Name]
                        ,[TypeId]
                        ,[Price])
                    VALUES
                        (@name
                        ,@typeId
                        ,@price)";
                return db.Execute(sql, newCandy) == 1;
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
        public bool EatCandy(Guid candyIdToDelete, Guid userIdWhoIsEating)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE
                            FROM UserCandy
                            WHERE ([CandyId] = @CandyId AND [UserId] = @UserId)";
                return db.Execute(sql, new { CandyId = candyIdToDelete, UserId = userIdWhoIsEating }) == 1;
            }
        }
        public User FavoriteCandy(Guid candyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * 
                            FROM [User]
                            WHERE [FavoriteTypeOfCandyId] = @candyId";
                var parameters = new { CandyId = candyId };
                var userToDonateTo = db.QueryFirst<User>(sql, parameters);
                return userToDonateTo;
            }

        }
        public bool DonateCandy(Guid candyIdToDonate, Guid userIdWhoIsDonating)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                EatCandy(candyIdToDonate, userIdWhoIsDonating);
                var userToDonate = FavoriteCandy(candyIdToDonate);
                var sql = @"INSERT INTO [UserCandy]
                            ([UserId], [CandyId])
                            VALUES
                                (@userId, @candyId)";
                return db.Execute(sql, new { CandyId = candyIdToDonate, UserId = userToDonate.Id }) == 1;
            }
        }
    }
}
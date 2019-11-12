using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
                   @"SELECT c.*, ct.Name as Type
                      FROM Candy c
                      JOIN Type ct
                      ON c.TypeId = ct.Id"
                   );
                return candies;
            }
        }

        public Candy GetCandyById(Guid candyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT c.*, ct.Name as Type
                            FROM Candy c
                              JOIN Type ct
                              ON c.TypeId = ct.Id
                            WHERE c.[Id] = @candyId";
                var parameters = new { candyId };
                var candyToReturn = db.QueryFirst<Candy>(sql, parameters);
                return candyToReturn;
            }
        }

        public bool DeleteCandy(Guid candyIdToDelete)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE
                            FROM [Candy]
                            WHERE [Id] = @candyIdToDelete";
                return db.Execute(sql, new { candyIdToDelete }) == 1;
            }
        }
        public Candy UpdateCandy(Guid candyIdToUpdate, Candy updatedCandy)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Candy]
                        SET [Name] = @name,
                            [TypeId] = @typeId,
                            [ImgUrl] = @imgUrl,
                            [Size] = @size
                        OUTPUT inserted.*
                        WHERE id = @id";
                updatedCandy.Id = candyIdToUpdate;
                var candy = db.QueryFirst<Candy>(sql, updatedCandy);
                return candy;
            }

        }
        public bool AddCandy(AddCandyDto newCandy)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT *
                            FROM [Candy]
                            WHERE ([Name] = @name AND [Size] = @size)";
                var parameters = new { name = newCandy.Name, size = newCandy.Size };
                var doubleCheck = db.Query<Candy>(sql, parameters).ToList();

                if (doubleCheck.Count() == 0)
                {
                    var sql2 = @"
                    INSERT INTO [Candy]
                        ([Name]
                        ,[TypeId]
                        ,[ImgUrl]
                        ,[Size])
                    VALUES
                        (@name
                        ,@typeId
                        ,@imgUrl
                        ,@size)";

                    return db.Execute(sql2, newCandy) == 1;
                }
                return false;
            }
        }

        public List<OwnedCandy> FetchUsersCandyList(User user)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT c.Id, c.Name, c.ImgUrl, c.TypeId, c.Size, uc.Id as UserCandyId, uc.IsUpForTrade, ct.Name as Type
                             FROM [UserCandy] uc
                                JOIN [Candy] c ON uc.CandyId = c.Id
                                JOIN [Type] ct ON c.TypeId = ct.Id
                             WHERE UserId = @userId";
                var parameters = new { userId = user.Id };
                var candies = db.Query<OwnedCandy>(sql, parameters).ToList();
                return candies;
            }
        }

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
        public Candy GetCandyFromDatabase(Guid userCandyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT c.*
                            FROM [UserCandy] uc
                                JOIN [Candy] c
                                ON c.[Id] = uc.[CandyId]
                            WHERE uc.[Id] = @userCandyId";
                var parameters = new { userCandyId };
                var candyIdToReturn = db.QueryFirst<Candy>(sql, parameters);
                return candyIdToReturn;
            }
        }

        public int GetCandyTypeId(Guid candyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT TypeId
                             FROM [Candy]
                             WHERE [Id] = @candyId";

                var candyTypeId = db.QueryFirst<int>(sql, new { CandyId = candyId });
                return candyTypeId;
            }

        }
    }
}
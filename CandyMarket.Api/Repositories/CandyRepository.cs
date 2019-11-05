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
                   @"SELECT * 
                      FROM Candy"
                   );
                return candies;
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
                        ,[Price]
                        ,[Size])
                    VALUES
                        (@name
                        ,@typeId
                        ,@price
                        ,@size)";

                    return db.Execute(sql2, newCandy) == 1;
                }
                return false;
            }
        }
    }
}
using System;
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

        public bool AddCandy(AddCandyDto newCandy)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO [Candy]
                        ([Name]
                        ,[TypeId]
                        ,[Price])
	                OUTPUT insterted.*
                    VALUES
                        (@name
                        ,@typeId
                        ,@price)";
                return db.Execute(sql, newCandy) == 1;
            }
        }

        public bool EatCandy(Guid candyIdToDelete)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE
                            FROM Candy
                            WHERE [Id] = @id";
                return db.Execute(sql, new { id = candyIdToDelete }) == 1;
            }
        }
    }
}
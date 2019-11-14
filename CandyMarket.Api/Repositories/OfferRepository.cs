using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        string _connectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";

        public IEnumerable<Offer> GetOffers()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                return db.Query<Offer>("SELECT * FROM [Offer]");
            }
        }

        public bool AddOffer(AddOfferDto newOffer)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Offer]
                            ([Offered]
                            ,[Requested]
                            ,[Message])
                        VALUES
                            (@offered
                            ,@requested
                            ,@message)";
                return db.Execute(sql, newOffer) == 1;
            }
        }

        public List<UsersOffersOut> FetchUsersOffersOut(User user)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"";
                var parameters = new { };
                var offersOut = db.Query<UsersOffersOut>(sql, parameters).ToList();
                return offersOut;
            }
        }

        public List<Offer> FetchUsersOffersIn(User user)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"";
                var parameters = new { };
                var offersIn = db.Query<Offer>(sql, parameters).ToList();
                return offersIn;  
            }
        }
    }
}

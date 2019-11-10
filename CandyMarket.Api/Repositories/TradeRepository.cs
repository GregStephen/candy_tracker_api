using CandyMarket.Api.DataModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        string _connectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";
        public IEnumerable<Trade> GetAllTrades()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var trades = db.Query<Trade>
                   (
                   @"SELECT * 
                      FROM Trade"
                   );
                return trades;
            }
        }
    }
}

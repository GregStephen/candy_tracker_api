using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CandyMarket.Api.DataModels;
using Dapper;
namespace CandyMarket.Api.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        string _connectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";

        public List<CandyType> GetCandyTypes()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                return  db.Query<CandyType>("SELECT * FROM [Type]").ToList();
            }
        }
    }
}

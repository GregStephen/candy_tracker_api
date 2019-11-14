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

        public List<UsersOffersOut> FetchUsersOffersOut(Guid userCandyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT o.Id,
		                            o.Message,
		                            uc.Id as OfferedUserCandyId,
		                            uc.CandyId as OfferedCandyId,
		                            c.ImgUrl as OfferedCandyImgUrl,
		                            c.[Name] as OfferedCandyName,
		                            c.Size as OfferedCandySize,
		                            t.[Name] as OfferedCandyType,
		                            uc2.CandyId as RequestedCandyId,
		                            uc2.Id as RequestedUserCandyId,
		                            uc2.UserId as RequestedUserId,
		                            u.FirstName as RequestedFirstName,
		                            u.LastName as RequestedLastName,
		                            c2.ImgUrl as RequestedCandyImgUrl,
		                            c2.[Name] as RequestedCandyName,
		                            c2.Size as RequestedCandySize,
		                            t2.[Name] as RequestedCandyType
                            FROM Offer o
	                            JOIN UserCandy uc
	                            ON o.Offered = uc.Id
	                            JOIN Candy c
	                            ON uc.CandyId = c.Id
	                            JOIN [Type] t
	                            ON c.TypeId = t.Id
	                            JOIN UserCandy uc2
	                            ON o.Requested = uc2.Id
	                            JOIN [User] u
	                            ON uc2.UserId = u.Id
	                            JOIN Candy c2
	                            ON uc2.CandyId = c2.Id
	                            Join [Type] t2
	                            ON c2.TypeId = t2.Id
                            WHERE o.Offered = @userCandyId";
                var parameters = new { userCandyId };
                var offersOut = db.Query<UsersOffersOut>(sql, parameters).ToList();
                return offersOut;
            }
        }

        public List<UsersOffersIn> FetchUsersOffersIn(Guid userCandyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT o.Id,
		                            o.Message,
		                            uc.Id as RequestedUserCandyId,
		                            uc.CandyId as RequestedCandyId,
		                            c.ImgUrl as RequestedCandyImgUrl,
		                            c.[Name] as RequestedCandyName,
		                            c.Size as RequestedCandySize,
		                            t.[Name] as RequestedCandyType,
		                            uc2.Id as OfferedUserCandyId,
		                            uc2.CandyId as OfferedCandyId,
		                            uc2.UserId as OfferedUserId,
		                            u.FirstName as OfferedFirstName,
		                            u.LastName as OfferedLastName,
		                            c2.ImgUrl as OfferedCandyImgUrl,
		                            c2.[Name] as OfferedCandyName,
		                            c2.Size as OfferedCandySize,
		                            t2.[Name] as OfferedCandyType
                            FROM Offer o
	                            JOIN UserCandy uc
	                            ON o.Offered = uc.Id
	                            JOIN Candy c
	                            ON uc.CandyId = c.Id
	                            JOIN [Type] t
	                            ON c.TypeId = t.Id
	                            JOIN UserCandy uc2
	                            ON o.Offered = uc2.Id
	                            JOIN [User] u
	                            ON uc2.UserId = u.Id
	                            JOIN Candy c2
	                            ON uc2.CandyId = c2.Id
	                            Join [Type] t2
	                            ON c2.TypeId = t2.Id
                            WHERE o.Requested = @userCandyId";
                var parameters = new { userCandyId };
                var offersIn = db.Query<UsersOffersIn>(sql, parameters).ToList();
                return offersIn;  
            }
        }
    }
}

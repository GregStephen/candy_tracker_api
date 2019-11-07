using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.DataModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public int FavoriteTypeOfCandyId { get; set; }
        public string FavoriteTypeOfCandyName { get; set; }
        public int AmountOfCandyEaten { get; set; }
        public int AmountOfCandyDonated { get; set; }
        public List<Candy> CandyOwned { get; set; }
    }
}

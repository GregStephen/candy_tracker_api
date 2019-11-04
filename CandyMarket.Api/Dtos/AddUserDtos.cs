using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.Dtos
{
    public class AddUserDtos
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FavoriteTypeOfCandy { get; set; }
        public int AmountOfCandyEaten { get; set; }
        public int AmountOfCandyDonated { get; set; }
    }
}

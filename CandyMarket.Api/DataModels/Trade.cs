using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.DataModels
{
    public class Trade
    {
        public Guid Id { get; set; }
        public string CandyName { get; set; }
        public string Type { get; set; }
        public string ImgUrl { get; set; }
        public string Size { get; set; }
        public Guid UserCandyId { get; set; }
        public bool IsUpForTrade { get; set; }
        public Guid CandyId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

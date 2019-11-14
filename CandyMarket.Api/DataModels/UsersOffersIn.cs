using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.DataModels
{
    public class UsersOffersIn
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string OfferedCandyName { get; set; }
        public string OfferedCandyType { get; set; }
        public string OfferedCandyImgUrl { get; set; }
        public string OfferedCandySize { get; set; }
        public string OfferedFirstName { get; set; }
        public string OfferedLastName { get; set; }
        public Guid OfferedUserId { get; set; }
        public Guid OfferedUserCandyId { get; set; }
        public Guid OfferedCandyId { get; set; }
        public string RequestedCandyName { get; set; }
        public string RequestedCandyType { get; set; }
        public string RequestedCandyImgUrl { get; set; }
        public string RequestedCandySize { get; set; }
        public Guid RequestedUserCandyId { get; set; }
    }
}

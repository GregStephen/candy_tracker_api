using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.DataModels
{
    public class UsersOffersOut
    {
        Guid Id { get; set; }
        public string OfferedCandyName { get; set; }
        public string OfferedCandyType { get; set; }
        public string OfferedCandyImgUrl { get; set; }
        public string OfferedCandySize { get; set; }
        public Guid OfferedUserCandyId { get; set; }
        public Guid OfferedCandyId { get; set; }
        public Guid RequestedUserId { get; set; }
        public string RequestedFirstName { get; set; }
        public string RequestedLastName { get; set; }
        public string RequestedCandyName { get; set; }
        public string RequestedCandyType { get; set; }
        public string ORequestedCandyImgUrl { get; set; }
        public string RequestedCandySize { get; set; }
        public Guid RequestedUserCandyId { get; set; }
    }
}

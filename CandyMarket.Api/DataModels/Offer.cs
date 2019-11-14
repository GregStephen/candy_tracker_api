using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.DataModels
{
    public class Offer
    {
        public Guid Id { get; set; }
        public Guid Offered { get; set; }
        public Guid Requested { get; set; }
        public string Message { get; set; }
    }
}

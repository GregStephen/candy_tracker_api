using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.Dtos
{
    public class AddOfferDto
    {
        public Guid Offered { get; set; }
        public Guid Requested { get; set; }
        public string Message { get; set; }
    }
}

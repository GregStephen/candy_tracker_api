using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.DataModels
{
    public class Trade
    {
        public Guid Id { get; set; }
        public Guid UserCandyId { get; set; }
    }
}

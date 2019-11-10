using CandyMarket.Api.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.Repositories
{
    public interface ITradeRepository
    {
        IEnumerable<Trade> GetAllTrades();
    }
}

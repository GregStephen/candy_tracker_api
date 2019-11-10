using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandyMarket.Api.Controllers
{
    [ApiController]
    [Route("trade")]

    public class TradeController : ControllerBase
    {
        private readonly ILogger<TradeController> _logger;
        private readonly ITradeRepository _repo;

        public TradeController(ILogger<TradeController> logger, ITradeRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        [HttpGet]
        public IEnumerable<Trade> GetAll()
        {
            return _repo.GetAllTrades();
        }
    }
}
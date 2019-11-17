using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.Api.Dtos;
using CandyMarket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandyMarket.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {

        private readonly ILogger<OfferController> _logger;
        private readonly IOfferRepository _repo;

        public OfferController(ILogger<OfferController> logger, IOfferRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        [HttpPost]
        public IActionResult TradeOffered(AddOfferDto newOffer)
        {
            if (_repo.AddOffer(newOffer))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{offerIdToDelete}")]
        public void Delete(Guid offerIdToDelete)
        {
            _repo.DeleteOffer(offerIdToDelete);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;
using CandyMarket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandyMarket.Api.Controllers
{
    [ApiController]
    [Route("candy")]
    public class CandyController : ControllerBase
    {
        private readonly ILogger<CandyController> _logger;
        private readonly ICandyRepository _repo;

        public CandyController(ILogger<CandyController> logger, ICandyRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Candy> GetAll()
        {
            return _repo.GetAllCandy();
        }

        [HttpGet("{candyId}")]
        public Candy Get(Guid candyId)
        {
            return _repo.GetAllCandy().FirstOrDefault(candy => candy.Id == candyId);
        }

        [HttpPost]
        public IActionResult Add(AddCandyDto newCandy)
        {
            if (_repo.AddCandy(newCandy))
            {
                return Created($"candy/{newCandy.Name}", newCandy);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateCandy(UpdateCandyDto updatedCandyCommand, Guid id)
        {

            var updatedCandy = new Candy()
            {
                Name = updatedCandyCommand.Name,
                Price = updatedCandyCommand.Price,
                TypeId = updatedCandyCommand.TypeId,
                Size = updatedCandyCommand.Size
            };

            var candyThatGotUpdated = _repo.UpdateCandy(id, updatedCandy);
            return Ok(candyThatGotUpdated);
        }
        [HttpDelete("{candyIdToDelete}")]
        public void Delete(Guid candyIdToDelete)
        {
            _repo.DeleteCandy(candyIdToDelete);
        }
        //[HttpPost]
        public void Trade()
        {
            /**
             * flex goal: Trade Candy
             * Hint: you're going to need to add Users to your application
             */
        }
    }
}

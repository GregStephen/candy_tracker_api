using System;
using System.Collections.Generic;
using System.Linq;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;
using CandyMarket.Api.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandyMarket.Api.Controllers
{
    [ApiController]
    [Route("candy")]
    [DisableCors]
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

        [HttpGet("user-candy/{userCandyId}")]
        public Candy GetCandyFromUserCandy (Guid userCandyId)
        {
            return _repo.GetCandyFromDatabase(userCandyId);
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
                ImgUrl = updatedCandyCommand.ImgUrl,
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
    }
}

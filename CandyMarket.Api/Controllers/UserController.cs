using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using CandyMarket.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CandyMarket.Api.DataModels;

namespace CandyMarket.Api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _repo;

        public UserController(ILogger<UserController> logger, IUserRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _repo.GetAllUsers();
        }

        [HttpPost]
        public IActionResult Add(AddUserDto newUser)
        {
            if (_repo.AddUser(newUser))
            {
                return Created($"user/{newUser.FirstName}", newUser);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("{userId}/buy/{candyId}")]
        public IActionResult Buy(Guid userId, Guid candyId)
        {
            if(_repo.BuyCandy(userId, candyId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("eat/{userCandyIdToDelete}")]
        public void Delete(Guid userCandyIdToDelete)
        {
            _repo.EatCandy(userCandyIdToDelete);
        }

        [HttpDelete("donate/{userCandyIdToDonate}")]
        public void Donate(Guid userCandyIdToDonate)
        {
            _repo.DonateCandy(userCandyIdToDonate);
        }
    }
}
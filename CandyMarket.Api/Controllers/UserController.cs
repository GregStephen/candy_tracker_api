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
        [HttpGet("trades")]
        public IEnumerable<Trade> GetAllTrades()
        {
            return _repo.GetAllTrades();
        }
        [HttpGet("{userId}")]
        public User Get(Guid userId)
        {
            return _repo.GetAllUsers().FirstOrDefault(user => user.Id == userId);
        }

        [HttpGet("{email}/p/{password}")]
        public User Get(string email, string password)
        {
            return _repo.GetUserByEmailAndPassword(email, password);
        }

        [HttpGet("user-candy/{userCandyId}")]
        public User GetUserFromUserCandy(Guid userCandyId)
        {
            return _repo.GetUserFromDatabase(userCandyId);
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
        [HttpDelete("{userIdToDelete}/eat/{userCandyIdToDelete}")]
        public void Delete(Guid userIdToDelete, Guid userCandyIdToDelete)
        {
            _repo.EatCandy(userIdToDelete, userCandyIdToDelete);
        }

        [HttpDelete("{userIdDonating}/donate/{userCandyIdToDonate}")]
        public void Donate(Guid userIdDonating, Guid userCandyIdToDonate)
        {
            _repo.DonateCandy(userIdDonating, userCandyIdToDonate);
            
        }

        [HttpPut("up-for-trade/{userCandyId}")]
        public IActionResult UpForTrade(Guid userCandyId)
        {
            if (_repo.PutCandyUpForTrade(userCandyId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("off-trade/{userCandyId}")]
        public IActionResult TakeOffTrade(Guid userCandyId)
        {
            if (_repo.TakeCandyOffTrade(userCandyId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{userCandyId1}/trades/{userCandyId2}")]
        public IActionResult Trade(Guid userCandyId1, Guid userCandyId2)
        {
            if (_repo.TradeCandy(userCandyId1, userCandyId2))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
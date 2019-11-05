using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using CandyMarket.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandyMarket.Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public void Add(AddUserDto newUser)
        {
            _repo.AddUser(newUser);
        }

        [HttpDelete("{candyIdToDelete}/eat")]
        public void Delete(Guid candyUserIdToDelete)
        {
            var candyId = _repo.GetCandyIdFromDatabase(candyUserIdToDelete);
            var userId = _repo.GetUserIdFromDatabase(candyUserIdToDelete);
            _repo.EatCandy(candyId, userId);
        }

        [HttpDelete("{candyIdToDonate}/donate")]
        public void Donate(Guid candyUserIdToDonate)
        {
            // todo: make this endpoint behave less greedy and more honest
            var candyId = _repo.GetCandyIdFromDatabase(candyUserIdToDonate);
            var userId = _repo.GetUserIdFromDatabase(candyUserIdToDonate);
            _repo.DonateCandy(candyId, userId);
        }
    }
}
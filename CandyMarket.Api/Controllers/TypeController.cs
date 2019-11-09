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
    [Route("[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ILogger<TypeController> _logger;
        private readonly ITypeRepository _repo;

        public TypeController(ILogger<TypeController> logger, ITypeRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        [HttpGet]
        public List<CandyType> GetAll()
        {
            return _repo.GetCandyTypes();
        }
    }
}
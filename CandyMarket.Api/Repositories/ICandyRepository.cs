using System;
using System.Collections.Generic;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;

namespace CandyMarket.Api.Repositories
{
    public interface ICandyRepository
    {
        IEnumerable<Candy> GetAllCandy();
        Candy GetCandyById(Guid candyId);
        bool AddCandy(AddCandyDto newCandy);
        bool DeleteCandy(Guid candyIdToDelete);
    }
}
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
        List<Candy> FetchUsersCandyList(User user);
        string FetchFavoriteCandyName(User user);
        bool AddCandy(AddCandyDto newCandy);
        bool DeleteCandy(Guid candyIdToDelete);
        Candy GetCandyFromDatabase(Guid userCandyId);
        Candy UpdateCandy(Guid candyIdToUpdate, Candy updatedCandy);
    }
}
using System;
using System.Collections.Generic;
using CandyMarket.Api.DataModels;
using CandyMarket.Api.Dtos;

namespace CandyMarket.Api.Repositories
{
    public interface ITypeRepository
    {
        List<CandyType> GetCandyTypes();
    }
}

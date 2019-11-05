using System;

namespace CandyMarket.Api.DataModels
{
    public class Candy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
    }
}

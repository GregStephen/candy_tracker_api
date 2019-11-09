using System;

namespace CandyMarket.Api.DataModels
{
    public class Candy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string ImgUrl { get; set; }
        public string Size { get; set; }
    }
}

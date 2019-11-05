namespace CandyMarket.Api.Dtos
{
    public class AddCandyDto
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
    }
}
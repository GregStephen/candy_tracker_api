﻿namespace CandyMarket.Api.Dtos
{
    public class AddUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int FavoriteTypeOfCandyId { get; set; }
    }
}

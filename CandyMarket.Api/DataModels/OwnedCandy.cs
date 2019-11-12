using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Api.DataModels
{
    public class OwnedCandy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string ImgUrl { get; set; }
        public string Size { get; set; }
        public Guid UserCandyId { get; set; }
        public bool IsUpForTrade { get; set; }
    }
}

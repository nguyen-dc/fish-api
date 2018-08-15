using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class CapitalCostModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime Month { get; set; }
        public decimal? PreviousCapitalCost { get; set; }
        public decimal? CapitalCost { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class LivestockHistoryDetailModel
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public int LivestockId { get; set; }
        public string LivestockName { get; set; }
        public decimal Weight { get; set; }
        public decimal Quantity { get; set; }
        public decimal MassAmount { get; set; }
        public decimal? DeadstockRatio { get; set; }
        public decimal? LivestockSize { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FLS.ServerSide.SharingObject
{
    public class CollectDeadstockRequest
    {
        public int DeadstockId { get; set; }
        public decimal MassAmount { get; set; }
        public decimal Ratio { get; set; }
        public int FishPondWarehouseId { get; set; }
        public DateTime? CollectDate { get; set; }
    }
}

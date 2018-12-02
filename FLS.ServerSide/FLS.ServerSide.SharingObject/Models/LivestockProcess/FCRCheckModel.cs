using System;

namespace FLS.ServerSide.SharingObject
{
    public class FCRCheckModel
    {
        public int FishPondWarehouseId { get; set; }
        public int LivestockId { get; set; }
        public decimal Weight { get; set; }
        public DateTime? CheckDate { get; set; }
    }
}

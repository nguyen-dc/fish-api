using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class FishPondModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FishPondTypeId { get; set; }
        public int FarmRegionId { get; set; }
        public decimal WaterSurfaceArea { get; set; }
        public decimal? A { get; set; }
        public decimal? B { get; set; }
        public decimal? C { get; set; }
        public decimal? D { get; set; }
        public decimal Depth { get; set; }
        public int? WarehouseId { get; set; }
    }
}

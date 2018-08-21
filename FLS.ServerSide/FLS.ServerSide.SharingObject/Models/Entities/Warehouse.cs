using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class WarehouseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FarmRegionId { get; set; }
        public int DefaultWarehouseId { get; set; }
        public int WarehouseTypeId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class ProductUnitProductModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductUnitId { get; set; }
        public string ProductUnitName { get; set; }
        public bool UnitHasScale { get; set; }
        public decimal DefaultUnitValue { get; set; }
    }
}

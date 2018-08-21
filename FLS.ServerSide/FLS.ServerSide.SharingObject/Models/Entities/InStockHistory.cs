using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class InStockHistoryModel
    {
        public int Id { get; set; }
        public DateTime MonthCode { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int ProductUnitId { get; set; }
        public decimal AmountExpect { get; set; }
        public decimal Amount { get; set; }
    }
}

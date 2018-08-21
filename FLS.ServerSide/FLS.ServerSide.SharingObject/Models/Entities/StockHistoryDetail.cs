using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockHistoryDetailModel
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductUnitId { get; set; }
        public decimal Amount { get; set; }
    }
}

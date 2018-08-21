using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockReceiveDocketDetailModel
    {
        public int Id { get; set; }
        public int StockReceiveDocketId { get; set; }
        public int? SupplierBranchId { get; set; }
        public string SupplierBranchName { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public int ProductUnitId { get; set; }
        public decimal UnitPrice { get; set; }
        public int VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockIssueDocketDetailModel
    {
        public int Id { get; set; }
        public int StockIssueDocketId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public int ProductUnitId { get; set; }
        public string ProductUnitName { get; set; }
        public decimal UnitPrice { get; set; }
        public int VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? CapitalCost { get; set; }
    }
}

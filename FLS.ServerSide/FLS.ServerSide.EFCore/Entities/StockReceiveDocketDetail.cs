using System;
using System.Collections.Generic;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class StockReceiveDocketDetail
    {
        public int Id { get; set; }
        public int StockReceiveDocketId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public int ProductUnitId { get; set; }
        public decimal UnitPrice { get; set; }
        public int VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

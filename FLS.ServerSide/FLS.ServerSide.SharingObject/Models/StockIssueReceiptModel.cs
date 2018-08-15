using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockIssueReceiptModel
    {
        public int Id { get; set; }
        public int StockIssueDocketId { get; set; }
        public int StockIssueReceiptTypeId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? UnpaidAmount { get; set; }
        public string BillCode { get; set; }
        public string BillSerial { get; set; }
        public string BillTemplateCode { get; set; }
    }
}

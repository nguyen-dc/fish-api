using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockIssueDocketModel
    {
        public int Id { get; set; }
        public int StockIssueDocketTypeId { get; set; }
        public int WarehouseId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DocketNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? CapitalCost { get; set; }
        public string Description { get; set; }
        public string ApproverCode { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ExecutorCode { get; set; }
        public DateTime ExecutedDate { get; set; }
        public int? StockReceiveDocketId { get; set; }
    }
}

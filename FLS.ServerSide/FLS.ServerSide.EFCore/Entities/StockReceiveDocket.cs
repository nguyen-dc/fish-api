using System;
using System.Collections.Generic;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class StockReceiveDocket
    {
        public int Id { get; set; }
        public int StockReceiveDocketTypeId { get; set; }
        public int WarehouseId { get; set; }
        public string DocketNumber { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public bool? IsActuallyReceived { get; set; }
        public string ApproverCode { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ExecutorCode { get; set; }
        public DateTime ExecutedDate { get; set; }
        public string ActuallyReceivedCode { get; set; }
        public DateTime ActuallyReceivedDate { get; set; }
        public int? StockIssueDocketId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

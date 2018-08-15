using System;
using System.Collections.Generic;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class StockReceivePaySlip
    {
        public int Id { get; set; }
        public int StockReceiveDocketId { get; set; }
        public int StockReceivePayslipTypeId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime PayslipDate { get; set; }
        public int SupplierBranchId { get; set; }
        public string SupplierBranchFullName { get; set; }
        public string SupplierBranchPhone { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? UnpaidAmount { get; set; }
        public string BillCode { get; set; }
        public string BillSerial { get; set; }
        public string BillTemplateCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

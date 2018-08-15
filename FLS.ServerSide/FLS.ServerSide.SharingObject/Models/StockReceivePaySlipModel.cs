using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockReceivePaySlipModel
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
    }
}

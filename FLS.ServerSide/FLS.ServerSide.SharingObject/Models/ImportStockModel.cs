using System;
using System.Collections.Generic;
using System.Text;

namespace FLS.ServerSide.SharingObject
{
    public class ImportStockModel
    {
        public StockReceiveDocketModel ReceiveDocket { get; set; }
        public List<ImportStockSupplierModel> Suppliers { get; set; }
        public List<ExpenditureDocketDetailModel> PaySlipDetails { get; set; }
    }
    public class ImportStockSupplierModel
    {
        public int SupplierBranchId { get; set; }
        public string BillCode { get; set; }
        public string BillSerial { get; set; }
        public string BillTemplateCode { get; set; }
        public List<StockReceiveDocketDetailModel> ReceiveDocketDetails { get; set; }
    }
}

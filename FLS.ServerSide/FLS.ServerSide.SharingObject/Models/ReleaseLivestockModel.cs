using System;
using System.Collections.Generic;
using System.Text;

namespace FLS.ServerSide.SharingObject
{
    public class ReleaseLivestockModel
    {
        public ReleaseLivestockDocketModel LivestockDocket { get; set; }
        public List<ImportStockSupplierModel> Suppliers { get; set; }
        public List<ExpenditureDocketDetailModel> PaySlipDetails { get; set; }
    }
    public class ReleaseLivestockDocketModel
    {
        public int FishPondWarehouseId { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public bool IsActuallyReceived { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
    }
}

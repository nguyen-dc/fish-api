using System;
using System.Collections.Generic;
using System.Text;

namespace FLS.ServerSide.SharingObject
{
    public class ReleaseLivestockModel
    {
        public ReleaseLivestockDocketModel LivestockDocket { get; set; }
        public ProductModel Livestock { get; set; }
        public List<ReleaseStockSupplierModel> Suppliers { get; set; }
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
    public class ReleaseStockSupplierModel
    {
        public int SupplierBranchId { get; set; }
        public string SupplierBranchName { get; set; }
        public string BillCode { get; set; }
        public string BillSerial { get; set; }
        public string BillTemplateCode { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal PricePerKg { get; set; }
        /// <summary>
        /// Tổng trọng lượng con giống (kg)
        /// </summary>
        public decimal MassAmount { get; set; }
        /// <summary>
        /// Kích cỡ con giống (gr/con)
        /// </summary>
        //[NotMapped]
        public decimal Size { get; set; }
        /// <summary>
        /// Số lượng con giống
        /// </summary>
        //[NotMapped]
        public decimal Quantity { get; set; }
    }
}

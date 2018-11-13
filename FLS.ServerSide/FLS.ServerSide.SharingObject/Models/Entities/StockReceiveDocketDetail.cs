using System.ComponentModel.DataAnnotations.Schema;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockReceiveDocketDetailModel
    {
        public int Id { get; set; }
        public int StockReceiveDocketId { get; set; }
        public int? SupplierBranchId { get; set; }
        public string SupplierBranchName { get; set; }
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
        /// <summary>
        /// Cho nhập con giống:
        /// Kích cỡ con giống 
        /// </summary>
        [NotMapped]
        public decimal LivestockSize { get; set; }
        /// <summary>
        /// Cho nhập con giống:
        /// Tổng trọng lượng cá giống (Kg)
        /// </summary>
        [NotMapped]
        public decimal LivestockQuantity { get; set; }
}
}

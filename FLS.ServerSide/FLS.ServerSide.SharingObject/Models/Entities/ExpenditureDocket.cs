using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class ExpenditureDocketModel
    {
        public int Id { get; set; }
        public int StockDocketId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime ExpendDate { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? UnpaidAmount { get; set; }
        public string BillCode { get; set; }
        public string BillSerial { get; set; }
        public string BillTemplateCode { get; set; }
    }
}

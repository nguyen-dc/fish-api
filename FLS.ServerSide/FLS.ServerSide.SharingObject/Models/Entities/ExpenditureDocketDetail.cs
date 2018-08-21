using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class ExpenditureDocketDetailModel
    {
        public int Id { get; set; }
        public int ExpenditureDocketId { get; set; }
        public int ExpenditureTypeId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

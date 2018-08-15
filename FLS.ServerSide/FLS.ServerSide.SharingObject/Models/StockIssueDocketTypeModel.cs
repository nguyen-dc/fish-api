using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockIssueDocketTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ReceiptNeeded { get; set; }
        public int ReceiptTypeId { get; set; }
        public bool ApprovalNeeded { get; set; }
        public int PickingPrice { get; set; }
        public bool IsSystem { get; set; }
        public string Description { get; set; }
    }
}

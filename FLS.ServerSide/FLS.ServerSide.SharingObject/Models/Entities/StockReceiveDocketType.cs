using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class StockReceiveDocketTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool PayslipNeeded { get; set; }
        public int PayslipTypeId { get; set; }
        public bool ApprovalNeeded { get; set; }
        public bool IsSystem { get; set; }
        public string Description { get; set; }
    }
}

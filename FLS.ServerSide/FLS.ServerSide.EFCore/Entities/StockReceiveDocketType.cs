using System;
using System.Collections.Generic;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class StockReceiveDocketType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool PayslipNeeded { get; set; }
        public int PayslipTypeId { get; set; }
        public bool ApprovalNeeded { get; set; }
        public bool IsSystem { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

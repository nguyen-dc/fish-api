using System;
using System.Collections.Generic;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class LivestockHistoryDetail
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public int LivestockId { get; set; }
        public string LivestockName { get; set; }
        public decimal Weight { get; set; }
        public decimal Quantity { get; set; }
        public decimal MassAmount { get; set; }
        public decimal? DeadstockRatio { get; set; }
        public decimal? LivestockSize { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

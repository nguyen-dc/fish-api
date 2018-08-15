using System;
using System.Collections.Generic;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class FeedConversionRate
    {
        public int Id { get; set; }
        public int FarmingSeasonId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime SurveyDate { get; set; }
        public decimal Weight { get; set; }
        public decimal Quantity { get; set; }
        public decimal MassAmount { get; set; }
        public decimal Fcr { get; set; }
        public bool IsAuto { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

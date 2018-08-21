using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class FeedConversionRateModel
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
        public int? LostPercent { get; set; }
        public bool? IsAuto { get; set; }
    }
}

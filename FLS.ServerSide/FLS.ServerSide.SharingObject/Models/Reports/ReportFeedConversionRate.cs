using System;

namespace FLS.ServerSide.SharingObject
{
    public class ReportFeedConversionRateRequest
    {
        public int FarmingSeasonId { get; set; }
    }
    public class ReportFeedConversionRate
    {
        public int FeedConversionRateId { get; set; }
        public int FishPondId { get; set; }
        public decimal WaterSurfaceArea { get; set; }
        public int FarmingSeasonId { get; set; }
        public DateTime SurveyDate { get; set; }
        public int ProductId { get; set; }
        public decimal Weight { get; set; }
        public decimal AverageQuantity { get; set; }
        public decimal DeadstockAmount { get; set; }
        public decimal DeadstockQuantity { get; set; }
        public decimal FoodQuantity { get; set; }
        public decimal LivestockAmount { get; set; }
        public decimal LivestockQuantity { get; set; }
        public decimal LostPercent { get; set; }
        public decimal ProprotionPercent { get; set; }
        public decimal Density { get; set; }
        public decimal Fcr { get; set; }
    }
}
using System;

namespace FLS.ServerSide.SharingObject
{
    public class ReportFarmingSeasonRequest
    {
        public int FarmRegionId { get; set; }
        public int FishPondId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class ReportFarmingSeason
    {
        public int FishPondId { get; set; }
        public string FishPondName { get; set; }
        public decimal A { get; set; }
        public decimal B { get; set; }
        public decimal C { get; set; }
        public decimal D { get; set; }
        public decimal WaterSurfaceArea { get; set; }
        public decimal Depth { get; set; }
        public int FarmingSeasonId { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionType { get; set; }
        public decimal HistoryId { get; set; }
        public decimal MassAmount { get; set; }
        public decimal Quantity { get; set; }
        public decimal AverageQuantity { get; set; }
        public decimal Density { get; set; }
        public DateTime ExpectedHarvestDate { get; set; }
        public decimal ExpectedHarvestQuantity { get; set; }

    }
}
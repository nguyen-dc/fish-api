using System;

namespace FLS.ServerSide.SharingObject
{
    public class ReportFarmingSeasonHistoryStockRequest
    {
        public int FarmingSeasonId { get; set; }
        public int ProductGroupId { get; set; }
        public int ProductSubgroupId { get; set; }
        public int ProductId { get; set; }

    }
    public class ReportFarmingSeasonHistoryStock
    {
        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }
        public int ProductSubgroupId { get; set; }
        public string ProductSubgroupName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductUnitId { get; set; }
        public string ProductUnitName { get; set; }
        public decimal Amount { get; set; }
        public decimal CapitalCost { get; set; }
    }
}
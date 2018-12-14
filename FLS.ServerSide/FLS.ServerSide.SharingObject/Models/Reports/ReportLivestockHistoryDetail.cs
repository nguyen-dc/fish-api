using System;
using System.Collections.Generic;
using System.Text;

namespace FLS.ServerSide.SharingObject
{
    public class ReportLivestockHistoryDetailRequest
    {
        public int FarmingSeasonId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class ReportLivestockHistoryDetail
    {
        public int ActionTypeId { get; set; }
        public string SeasonName { get; set; }
        public int FarmHistoryId { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionType { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductUnitId { get; set; }
        public string ProductUnitName { get; set; }
        public decimal Weight { get; set; }
        public decimal Quantity { get; set; }
        public decimal MassAmount { get; set; }
        public decimal DeadstockRatio { get; set; }
        public decimal QtyFood { get; set; }
        public decimal QtyMedicine { get; set; }
        public string MedicineName { get; set; }
        public decimal QtyDeadStock { get; set; }
        public decimal DeadStockMassAmount { get; set; }
    }
}
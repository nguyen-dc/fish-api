using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class FarmingSeasonHistoryModel
    {
        public int Id { get; set; }
        public int FarmingSeasonId { get; set; }
        public DateTime ActionDate { get; set; }
        public int ActionType { get; set; }
        public string Description { get; set; }
    }
}

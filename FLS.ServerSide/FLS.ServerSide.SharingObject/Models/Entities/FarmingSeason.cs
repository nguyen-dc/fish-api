using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class FarmingSeasonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FishPondId { get; set; }
        public DateTime StartFarmDate { get; set; }
        public DateTime FinishFarmDateExpected { get; set; }
        public DateTime? FinishFarmDate { get; set; }
        public bool? IsFinished { get; set; }
    }
}

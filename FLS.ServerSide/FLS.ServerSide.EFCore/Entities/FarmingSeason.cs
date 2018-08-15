using System;
using System.Collections.Generic;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class FarmingSeason
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FishPondId { get; set; }
        public DateTime StartFarmDate { get; set; }
        public DateTime FinishFarmDateExpected { get; set; }
        public DateTime? FinishFarmDate { get; set; }
        public bool? IsFinished { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

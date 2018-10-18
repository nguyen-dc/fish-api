﻿using System;
using System.Collections.Generic;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductGroupId { get; set; }
        public int ProductSubgroupId { get; set; }
        public int DefaultUnitId { get; set; }
        public int TaxPercent { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

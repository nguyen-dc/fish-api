using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class ProductSubgroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductGroupId { get; set; }
        public string Description { get; set; }
    }
}

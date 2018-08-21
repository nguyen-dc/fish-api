using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class ProductGroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public string Description { get; set; }
    }
}

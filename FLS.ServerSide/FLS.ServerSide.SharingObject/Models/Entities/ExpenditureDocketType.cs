using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class ExpenditureDocketTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsReceipt { get; set; }
        public string Description { get; set; }
    }
}

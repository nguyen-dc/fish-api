using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public partial class SupplierBranchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SupplierId { get; set; }
        public bool? IsMain { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
    }
}

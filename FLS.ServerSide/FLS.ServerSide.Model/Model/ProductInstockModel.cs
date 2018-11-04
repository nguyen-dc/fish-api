using System;
using System.Collections.Generic;
using System.Text;

namespace FLS.ServerSide.Model
{
    public class ProductInstockModel
    {
        public int ProductId {get;set;}
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public int ProductUnitId { get; set; }
    }
}

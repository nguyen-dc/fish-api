using System;
using System.Collections.Generic;
using System.Text;

namespace FLS.ServerSide.SharingObject
{
    public class ExportStockModel
    {
        public StockIssueDocketModel IssueDocket { get; set; }
        public List<StockIssueDocketDetailModel> DocketDetails { get; set; }
        public ExpenditureDocketModel Receipt { get; set; }
    }
    public class ExportStockDetailModel
    {
        public StockIssueDocketModel IssueDocket { get; set; }
        public List<StockIssueDocketDetailModel> Details { get; set; }
    }
}

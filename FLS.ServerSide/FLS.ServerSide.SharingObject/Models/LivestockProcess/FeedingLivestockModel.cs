﻿using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public class FeedingLivestockModel
    {
        public int FishPondWarehouseId { get; set; }
        public DateTime? FeedDate { get; set; }
        public List<StockIssueDocketDetailModel> Details { get; set; }
    }
}

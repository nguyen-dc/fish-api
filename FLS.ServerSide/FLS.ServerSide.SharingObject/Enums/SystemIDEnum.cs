using System;
using System.Collections.Generic;
using System.Text;

namespace FLS.ServerSide.SharingObject
{
    public enum SystemIDEnum
    {
        /// <summary>
        /// loại phiếu nhập: nhập hàng hóa
        /// </summary>
        ImportStock_TypeDefault = 2,
        /// <summary>
        /// loại phiếu xuất: xuất hàng hóa
        /// </summary>
        ExportStock_TypeDefault = 1,

        #region ReleaseLivestock
        /// <summary>
        /// loại phiếu nhập: nhập thả cá
        /// </summary>
        ReleaseLiveStock_ReceiveType = 4,
        /// <summary>
        /// Loại chi phí: nhập thả cá
        /// </summary>
        ReleaseLiveStock_ExpenditureType = 2,
        /// <summary>
        /// Loại phiếu xuất: nhập thả cá
        /// </summary>
        ReleaseLiveStock_IssueType = 6,
        #endregion

        #region FeedingLivestock
        /// <summary>
        /// Loại phiếu xuất: xuất chuyển nội bộ (cho ăn/uống thuốc/chuyển kho)
        /// </summary>
        FeedingLivestock_IssueType = 4,
        #endregion
        #region ProductGroup
        /// <summary>
        /// Ngành hàng giống nuôi
        /// </summary>
        ProductGroup_LivestockSeed = 1,
        ProductGroup_Food = 2,
        ProductGroup_Medicine = 3,
        #endregion

        #region FarmingSeason
        /// <summary>
        /// Lịch sử đợt nuôi: 
        /// Action thả cá (nhập con giống)
        /// </summary>
        FarmingSeason_ActionType_Release = 1,
        /// <summary>
        /// Lịch sử đợt nuôi: 
        /// Action xuất cá (xuất con giống)
        /// </summary>
        FarmingSeason_ActionType_Export = 2,
        /// <summary>
        /// Lịch sử đợt nuôi: 
        /// Action Cho ăn
        /// </summary>
        FarmingSeason_ActionType_Feed = 3,
        /// <summary>
        /// Lịch sử đợt nuôi: 
        /// Action Rải thuốc
        /// </summary>
        FarmingSeason_ActionType_Medicine = 4,
        /// <summary>
        /// Lịch sử đợt nuôi: 
        /// Action cân trọng
        /// </summary>
        FarmingSeason_ActionType_FCR = 5,
        #endregion
    }
}

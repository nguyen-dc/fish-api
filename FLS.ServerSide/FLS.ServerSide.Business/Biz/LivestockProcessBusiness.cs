using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.Model;
using FLS.ServerSide.Model.Scope;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class LivestockProcessBusiness : ILivestockProcessBusiness
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        private readonly IStockReceiveDocketService svcStockReceiveDocket;
        private readonly IStockReceiveDocketDetailService svcStockReceiveDocketDetail;
        private readonly IExpenditureDocketService svcExpenditureDocket;
        private readonly IExpenditureDocketDetailService svcExpenditureDocketDetail;
        private readonly ICurrentInStockService svcCurrentInStock;
        private readonly IFarmingSeasonService svcFarmingSeason;
        private readonly IFishPondService svcFishPond;
        private readonly IWarehouseService svcWarehouse;
        private readonly IStockIssueDocketService svcStockIssueDocket;
        private readonly IStockIssueDocketDetailService svcStockIssueDocketDetail;
        private readonly IFeedConversionRateService svcFeedConversionRate;
        private readonly IFarmingSeasonHistoryService svcFarmingSeasonHistory;
        private readonly ILivestockHistoryDetailService svcLivestockHistoryDetail;
        private readonly IStockHistoryDetailService svcStockHistoryDetail;
        private readonly IProductService svcProduct;
        private readonly IMapper iMapper;
        public LivestockProcessBusiness(
            FLSDbContext _context,
            IScopeContext _scopeContext,
            IStockReceiveDocketService _svcStockReceiveDocket,
            IStockReceiveDocketDetailService _svcStockReceiveDocketDetail,
            IExpenditureDocketService _svcExpenditureDocket,
            IExpenditureDocketDetailService _svcExpenditureDocketDetail,
            ICurrentInStockService _svcCurrentInStock,
            IFarmingSeasonService _svcFarmingSeason,
            IFishPondService _svcFishPond,
            IWarehouseService _svcWarehouse,
            IStockIssueDocketService _svcStockIssueDocket,
            IStockIssueDocketDetailService _svcStockIssueDocketDetail,
            IFeedConversionRateService _svcFeedConversionRate,
            IFarmingSeasonHistoryService _svcFarmingSeasonHistory,
            ILivestockHistoryDetailService _svcLivestockHistoryDetail,
            IStockHistoryDetailService _svcStockHistoryDetail,
            IProductService _svcProduct,
            IMapper _iMapper)
        {
            context = _context;
            scopeContext = _scopeContext;
            svcStockReceiveDocket = _svcStockReceiveDocket;
            svcStockReceiveDocketDetail = _svcStockReceiveDocketDetail;
            svcExpenditureDocket = _svcExpenditureDocket;
            svcExpenditureDocketDetail = _svcExpenditureDocketDetail;
            svcCurrentInStock = _svcCurrentInStock;
            svcFarmingSeason = _svcFarmingSeason;
            svcFishPond = _svcFishPond;
            svcWarehouse = _svcWarehouse;
            svcStockIssueDocket = _svcStockIssueDocket;
            svcStockIssueDocketDetail = _svcStockIssueDocketDetail;
            svcFeedConversionRate = _svcFeedConversionRate;
            svcFarmingSeasonHistory = _svcFarmingSeasonHistory;
            svcLivestockHistoryDetail = _svcLivestockHistoryDetail;
            svcStockHistoryDetail = _svcStockHistoryDetail;
            svcProduct = _svcProduct;
            iMapper = _iMapper;
        }
        public async Task<int> ReleaseLivestock(ReleaseLivestockModel _model)
        {
            if (_model == null || _model.LivestockDocket == null || _model.Livestock == null || _model.Suppliers == null)
            {
                scopeContext.AddError("Lỗi dữ liệu đầu vào");
                return 0;
            }
            if (_model.LivestockDocket.FishPondWarehouseId <= 0)
            {
                scopeContext.AddError("Chưa chọn ao thả giống");
                return 0;
            }
            // dữ liệu ao
            var thisFishPond = await svcFishPond.GetByWarehouseId(_model.LivestockDocket.FishPondWarehouseId);
            if (thisFishPond == null)
            {
                scopeContext.AddError("Lỗi dữ liệu kho-ao " + _model.LivestockDocket.FishPondWarehouseId);
                return 0;
            }
            // đợt nuôi
            var thisFarmingSeason = await svcFarmingSeason.GetByFishPondId(thisFishPond.Id);
            if (thisFarmingSeason == null)
            {
                scopeContext.AddError("Ao này chưa vào đợt nuôi");
                return 0;
            }
            // dữ liệu kho-ao
            var thisFishPondWarehouse = await svcWarehouse.GetDetail(_model.LivestockDocket.FishPondWarehouseId);
            if (thisFishPondWarehouse == null || thisFishPondWarehouse.DefaultWarehouseId <= 0)
            {
                scopeContext.AddError("Lỗi dữ liệu kho mặc định cho ao");
                return 0;
            }
            // bắt đầu tạo phiếu
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Phiếu nhập kho mặc định
                    StockReceiveDocket docket = new StockReceiveDocket();
                    docket.Description = _model.LivestockDocket.Description;
                    docket.IsActuallyReceived = _model.LivestockDocket.IsActuallyReceived;
                    docket.ReceiveDate = _model.LivestockDocket.ReceiveDate;
                    docket.StockReceiveDocketTypeId = (int)SystemIDEnum.ReleaseLiveStock_ReceiveType;
                    docket.WarehouseId = thisFishPondWarehouse.DefaultWarehouseId;
                    docket.ExecutorCode = scopeContext.UserCode;
                    docket.ExecutedDate = DateTime.UtcNow;
                    if (docket.IsActuallyReceived.GetValueOrDefault(false))
                    {
                        docket.ActuallyReceivedDate = docket.ExecutedDate;
                        docket.ActuallyReceivedCode = docket.ExecutorCode;
                    }
                    docket.Id = await svcStockReceiveDocket.Add(docket);
                    // Phiếu xuất kho mặc định
                    StockIssueDocket issueDocket = new StockIssueDocket();
                    issueDocket.CustomerId = thisFishPondWarehouse.Id;
                    issueDocket.CustomerName = thisFishPond.Name;
                    issueDocket.Description = "Thả cá giống";
                    issueDocket.ExecutorCode = scopeContext.UserCode;
                    issueDocket.ExecutedDate = DateTime.UtcNow;
                    issueDocket.IssueDate = _model.LivestockDocket.ReceiveDate;
                    issueDocket.StockIssueDocketTypeId = (int)SystemIDEnum.ReleaseLiveStock_IssueType;
                    issueDocket.StockReceiveDocketId = docket.Id;
                    issueDocket.WarehouseId = docket.WarehouseId;
                    issueDocket.Id = await svcStockIssueDocket.Add(issueDocket);
                    // Lịch sử đợt nuôi (master lịch sử ao nuôi)
                    FarmingSeasonHistory history = new FarmingSeasonHistory();
                    history.ActionDate = _model.LivestockDocket.ReceiveDate.GetValueOrDefault(DateTime.UtcNow);
                    history.ActionType = (int)SystemIDEnum.FarmingSeason_ActionType_Release;
                    history.Description = "Nhập thả con giống";
                    history.FarmingSeasonId = thisFarmingSeason.Id;
                    history.Id = await svcFarmingSeasonHistory.Add(history);

                    decimal orderVAT = 0;
                    decimal orderAmount = 0;
                    decimal orderTotalAmount = 0;
                    decimal livestockMassAmount = 0;
                    decimal livestockQuantity = 0;
                    //List<FeedConversionRate> fcrList = new List<FeedConversionRate>();
                    foreach (ReleaseStockSupplierModel item in _model.Suppliers)
                    {
                        // Chi tiết phiếu nhập
                        StockReceiveDocketDetail docketDetail = new StockReceiveDocketDetail();
                        docketDetail.ProductId = _model.Livestock.Id;
                        docketDetail.ProductUnitId = _model.Livestock.DefaultUnitId;
                        docketDetail.Quantity = item.MassAmount;
                        docketDetail.UnitPrice = item.PricePerKg;
                        docketDetail.VatPercent = _model.Livestock.TaxPercent;
                        docketDetail.StockReceiveDocketId = docket.Id;
                        docketDetail.SupplierBranchId = item.SupplierBranchId;
                        docketDetail.SupplierBranchName = item.SupplierBranchName;
                        docketDetail.Amount = item.MassAmount * item.PricePerKg;
                        docketDetail.Vat = docketDetail.Amount * (_model.Livestock.TaxPercent / (decimal)100);
                        docketDetail.TotalAmount = docketDetail.Amount + docketDetail.Vat;
                        docketDetail.Id = await svcStockReceiveDocketDetail.Add(docketDetail);

                        // Chi tiết phiếu xuất
                        StockIssueDocketDetail issueDocketDetail = new StockIssueDocketDetail();
                        issueDocketDetail.StockIssueDocketId = issueDocket.Id;
                        issueDocketDetail.ProductId = docketDetail.ProductId;
                        issueDocketDetail.ProductUnitId = docketDetail.ProductUnitId;
                        issueDocketDetail.Quantity = docketDetail.Quantity;
                        issueDocketDetail.UnitPrice = docketDetail.UnitPrice;
                        issueDocketDetail.VatPercent = docketDetail.VatPercent;
                        issueDocketDetail.Amount = docketDetail.Amount;
                        issueDocketDetail.Vat = docketDetail.Vat;
                        issueDocketDetail.TotalAmount = docketDetail.TotalAmount;
                        issueDocketDetail.Id = await svcStockIssueDocketDetail.Add(issueDocketDetail);

                        ExpenditureDocket exp = null;
                        exp = new ExpenditureDocket();
                        exp.StockDocketId = docket.Id;
                        exp.PartnerId = item.SupplierBranchId;
                        exp.PartnerName = item.SupplierBranchName;
                        exp.WarehouseId = docket.WarehouseId;
                        exp.BillCode = item.BillCode;
                        exp.BillSerial = item.BillSerial;
                        exp.BillTemplateCode = item.BillTemplateCode;
                        exp.BillDate = item.BillDate;
                        exp.CreatedUser = scopeContext.UserCode;
                        exp.IsReceipt = false;
                        exp.ExpendDate = docket.ExecutedDate;
                        exp.Amount = docketDetail.Amount;
                        exp.TotalAmount = docketDetail.TotalAmount;
                        exp.Vat = docketDetail.Vat;
                        exp.Id = await svcExpenditureDocket.Add(exp);

                        // Chi tiết phiếu chi
                        ExpenditureDocketDetail eD = new ExpenditureDocketDetail();
                        eD.ExpenditureDocketId = exp.Id;
                        eD.VatPercent = docketDetail.VatPercent;
                        eD.Amount = docketDetail.Amount;
                        eD.Vat = docketDetail.Vat;
                        eD.TotalAmount = docketDetail.TotalAmount;
                        eD.ProductId = docketDetail.ProductId;
                        eD.ExpenditureTypeId = (int)SystemIDEnum.ReleaseLiveStock_ExpenditureType;
                        eD.Id = await svcExpenditureDocketDetail.Add(eD);

                        // Lịch sử con giống (detail lịch sử ao nuôi)
                        LivestockHistoryDetail historyDetail = new LivestockHistoryDetail();
                        historyDetail.HistoryId = history.Id;
                        historyDetail.LivestockId = _model.Livestock.Id;
                        historyDetail.Quantity = item.Quantity;
                        historyDetail.MassAmount = item.MassAmount; // kg
                        historyDetail.LivestockSize = item.Size; // con/kg = (MassAmount / Quantity) * 1000
                        historyDetail.Weight = 1000 / (historyDetail.Quantity / historyDetail.MassAmount); // gram/con
                        historyDetail.Id = await svcLivestockHistoryDetail.Add(historyDetail);

                        orderVAT += docketDetail.Vat;
                        orderAmount += docketDetail.Amount;
                        orderTotalAmount += docketDetail.TotalAmount;
                        livestockMassAmount += item.MassAmount;
                        livestockQuantity += item.Quantity;
                    }
                    // cập nhật phiếu xuất
                    issueDocket.Vat = orderVAT;
                    issueDocket.Amount = orderAmount;
                    issueDocket.TotalAmount = orderTotalAmount;
                    await svcStockIssueDocket.Modify(issueDocket);

                    //Tỷ lệ tăng trọng
                    FeedConversionRate fcr = new FeedConversionRate();
                    fcr.FarmingSeasonId = thisFarmingSeason.Id;
                    fcr.IsAuto = false;
                    fcr.ProductId = _model.Livestock.Id;
                    fcr.SurveyDate = _model.LivestockDocket.ReceiveDate.GetValueOrDefault(DateTime.UtcNow);
                    fcr.Quantity = livestockQuantity; // số lượng cá
                    fcr.MassAmount = livestockMassAmount; // tổng kg cá
                    fcr.Weight = 1000 / (fcr.Quantity / fcr.MassAmount); // gram/con
                    fcr.ProductName = _model.Livestock.Name;
                    fcr.Id = await svcFeedConversionRate.Add(fcr);
                    
                    // nếu có chi phí phát sinh, tạo phiếu chi
                    if (_model.PaySlipDetails != null && _model.PaySlipDetails.Count > 0)
                    {
                        ExpenditureDocket expendDocket = new ExpenditureDocket();
                        expendDocket.StockDocketId = docket.Id;
                        expendDocket.WarehouseId = docket.WarehouseId;
                        expendDocket.CreatedUser = scopeContext.UserCode;
                        expendDocket.ExpendDate = docket.ExecutedDate;
                        expendDocket.Amount = 0;
                        expendDocket.TotalAmount = 0;
                        expendDocket.Vat = 0;
                        expendDocket.Id = await svcExpenditureDocket.Add(expendDocket);

                        foreach (ExpenditureDocketDetailModel item in _model.PaySlipDetails)
                        {
                            ExpenditureDocketDetail eD = iMapper.Map<ExpenditureDocketDetail>(item);
                            eD.ExpenditureDocketId = expendDocket.Id;
                            eD.Id = await svcExpenditureDocketDetail.Add(eD);

                            expendDocket.Amount += eD.Amount;
                            expendDocket.TotalAmount += eD.TotalAmount;
                            expendDocket.Vat += eD.Vat;
                        }
                        await svcExpenditureDocket.Modify(expendDocket);
                        orderVAT += expendDocket.Vat;
                        orderAmount += expendDocket.Amount;
                        orderTotalAmount += expendDocket.TotalAmount;
                    }
                    // cập nhật phiếu nhập
                    docket.Vat = orderVAT;
                    docket.Amount = orderAmount;
                    docket.TotalAmount = orderTotalAmount;
                    await svcStockReceiveDocket.Modify(docket);

                    transaction.Commit();
                    return docket.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
        public async Task<int> FeedingLivestock(FeedingLivestockModel _model)
        {
            if (_model.FishPondWarehouseId <= 0)
            {
                scopeContext.AddError("Chưa chọn ao cho ăn");
                return 0;
            }
            // dữ liệu ao
            var thisFishPond = await svcFishPond.GetByWarehouseId(_model.FishPondWarehouseId);
            if (thisFishPond == null)
            {
                scopeContext.AddError("Lỗi dữ liệu kho-ao " + _model.FishPondWarehouseId);
                return 0;
            }
            // đợt nuôi
            var thisFarmingSeason = await svcFarmingSeason.GetByFishPondId(thisFishPond.Id);
            if (thisFarmingSeason == null)
            {
                scopeContext.AddError("Ao này chưa vào đợt nuôi");
                return 0;
            }
            // dữ liệu kho-ao
            var thisFishPondWarehouse = await svcWarehouse.GetDetail(_model.FishPondWarehouseId);
            if (thisFishPondWarehouse == null || thisFishPondWarehouse.DefaultWarehouseId <= 0)
            {
                scopeContext.AddError("Lỗi dữ liệu kho mặc định cho ao");
                return 0;
            }
            _model.FeedDate = _model.FeedDate.GetValueOrDefault(DateTime.UtcNow);
            // bắt đầu tạo phiếu
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Phiếu xuất kho mặc định
                    StockIssueDocket issueDocket = new StockIssueDocket();
                    issueDocket.CustomerId = _model.FishPondWarehouseId;
                    issueDocket.CustomerName = thisFishPond.Name;
                    issueDocket.Description = "Cho ăn";
                    issueDocket.ExecutedDate = _model.FeedDate.Value;
                    issueDocket.ExecutorCode = scopeContext.UserCode;
                    issueDocket.IssueDate = _model.FeedDate;
                    issueDocket.StockIssueDocketTypeId = (int)SystemIDEnum.FeedingLivestock_IssueType;
                    issueDocket.WarehouseId = thisFishPondWarehouse.DefaultWarehouseId;
                    issueDocket.Id = await svcStockIssueDocket.Add(issueDocket);
                    // Lịch sử đợt nuôi (master lịch sử ao nuôi)
                    FarmingSeasonHistory history = new FarmingSeasonHistory();
                    history.ActionDate = _model.FeedDate.Value;
                    history.ActionType = (int)SystemIDEnum.FarmingSeason_ActionType_Feed;
                    history.Description = "Cho ăn";
                    history.FarmingSeasonId = thisFarmingSeason.Id;
                    history.Id = await svcFarmingSeasonHistory.Add(history);
                    // Chi tiết phiếu xuất kho mặc định
                    List<StockIssueDocketDetail> docketDetails = iMapper.Map<List<StockIssueDocketDetail>>(_model.Details);
                    decimal orderVAT = 0;
                    decimal orderAmount = 0;
                    decimal orderTotalAmount = 0;
                    foreach (var item in docketDetails)
                    {
                        item.StockIssueDocketId = issueDocket.Id;
                        item.UnitPrice = 0;
                        item.Amount = item.Quantity * item.UnitPrice;
                        item.Vat = item.Amount * (item.VatPercent / (decimal)100);
                        item.TotalAmount = item.Amount + item.Vat;
                        item.Id = await svcStockIssueDocketDetail.Add(item);

                        orderVAT += item.Vat;
                        orderAmount += item.Amount;
                        orderTotalAmount += item.TotalAmount;

                        #region Trừ vào danh sách tồn - Tạm thời chưa chuyển đổi sang số lượng theo đơn vị tính chuẩn
                        var instock = await svcCurrentInStock.GetList(issueDocket.WarehouseId, item.ProductId);
                        if (instock == null || instock.Count == 0)
                        {
                            CurrentInStock cis = new CurrentInStock()
                            {
                                Amount = 0 - item.Quantity,
                                AmountExpect = 0 - item.Quantity,
                                ProductId = item.ProductId,
                                ProductUnitId = item.ProductUnitId,
                                WarehouseId = issueDocket.WarehouseId
                            };
                            cis.Id = await svcCurrentInStock.Add(cis);
                        }
                        else
                        {
                            CurrentInStock cis = instock[0];
                            cis.Amount -= item.Quantity;
                            cis.AmountExpect -= item.Quantity;
                            await svcCurrentInStock.Modify(cis);
                        }
                        #endregion
                        // Lịch sử hàng hóa
                        StockHistoryDetail historyDetail = new StockHistoryDetail();
                        historyDetail.HistoryId = history.Id;
                        historyDetail.ProductId = item.ProductId;
                        historyDetail.Amount = item.Quantity;
                        historyDetail.ProductUnitId = item.ProductUnitId; // kg
                        historyDetail.Id = await svcStockHistoryDetail.Add(historyDetail);
                    }
                    issueDocket.Vat = orderVAT;
                    issueDocket.Amount = orderAmount;
                    issueDocket.TotalAmount = orderTotalAmount;
                    await svcStockIssueDocket.Modify(issueDocket);

                    transaction.Commit();
                    return issueDocket.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
        public async Task<int> CuringLivestock(FeedingLivestockModel _model)
        {
            if (_model.FishPondWarehouseId <= 0)
            {
                scopeContext.AddError("Chưa chọn ao rải thuốc");
                return 0;
            }
            // dữ liệu ao
            var thisFishPond = await svcFishPond.GetByWarehouseId(_model.FishPondWarehouseId);
            if (thisFishPond == null)
            {
                scopeContext.AddError("Lỗi dữ liệu kho-ao " + _model.FishPondWarehouseId);
                return 0;
            }
            // đợt nuôi
            var thisFarmingSeason = await svcFarmingSeason.GetByFishPondId(thisFishPond.Id);
            if (thisFarmingSeason == null)
            {
                scopeContext.AddError("Ao này chưa vào đợt nuôi");
                return 0;
            }
            // dữ liệu kho-ao
            var thisFishPondWarehouse = await svcWarehouse.GetDetail(_model.FishPondWarehouseId);
            if (thisFishPondWarehouse == null || thisFishPondWarehouse.DefaultWarehouseId <= 0)
            {
                scopeContext.AddError("Lỗi dữ liệu kho mặc định cho ao");
                return 0;
            }
            _model.FeedDate = _model.FeedDate.GetValueOrDefault(DateTime.UtcNow);
            // bắt đầu tạo phiếu
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Phiếu xuất kho mặc định
                    StockIssueDocket issueDocket = new StockIssueDocket();
                    issueDocket.CustomerId = _model.FishPondWarehouseId;
                    issueDocket.CustomerName = thisFishPond.Name;
                    issueDocket.Description = "Rải thuốc";
                    issueDocket.ExecutedDate = _model.FeedDate.Value;
                    issueDocket.ExecutorCode = scopeContext.UserCode;
                    issueDocket.IssueDate = _model.FeedDate;
                    issueDocket.StockIssueDocketTypeId = (int)SystemIDEnum.FeedingLivestock_IssueType;
                    issueDocket.WarehouseId = thisFishPondWarehouse.DefaultWarehouseId;
                    issueDocket.Id = await svcStockIssueDocket.Add(issueDocket);
                    // Lịch sử đợt nuôi (master lịch sử ao nuôi)
                    FarmingSeasonHistory history = new FarmingSeasonHistory();
                    history.ActionDate = _model.FeedDate.Value;
                    history.ActionType = (int)SystemIDEnum.FarmingSeason_ActionType_Medicine;
                    history.Description = "Rải thuốc";
                    history.FarmingSeasonId = thisFarmingSeason.Id;
                    history.Id = await svcFarmingSeasonHistory.Add(history);
                    // Chi tiết phiếu xuất kho mặc định
                    List<StockIssueDocketDetail> docketDetails = iMapper.Map<List<StockIssueDocketDetail>>(_model.Details);
                    decimal orderVAT = 0;
                    decimal orderAmount = 0;
                    decimal orderTotalAmount = 0;
                    foreach (var item in docketDetails)
                    {
                        item.StockIssueDocketId = issueDocket.Id;
                        item.UnitPrice = 0;
                        item.Amount = item.Quantity * item.UnitPrice;
                        item.Vat = item.Amount * (item.VatPercent / (decimal)100);
                        item.TotalAmount = item.Amount + item.Vat;
                        item.Id = await svcStockIssueDocketDetail.Add(item);

                        orderVAT += item.Vat;
                        orderAmount += item.Amount;
                        orderTotalAmount += item.TotalAmount;

                        #region Trừ vào danh sách tồn - Tạm thời chưa chuyển đổi sang số lượng theo đơn vị tính chuẩn
                        var instock = await svcCurrentInStock.GetList(issueDocket.WarehouseId, item.ProductId);
                        if (instock == null || instock.Count == 0)
                        {
                            CurrentInStock cis = new CurrentInStock()
                            {
                                Amount = 0 - item.Quantity,
                                AmountExpect = 0 - item.Quantity,
                                ProductId = item.ProductId,
                                ProductUnitId = item.ProductUnitId,
                                WarehouseId = issueDocket.WarehouseId
                            };
                            cis.Id = await svcCurrentInStock.Add(cis);
                        }
                        else
                        {
                            CurrentInStock cis = instock[0];
                            cis.Amount -= item.Quantity;
                            cis.AmountExpect -= item.Quantity;
                            await svcCurrentInStock.Modify(cis);
                        }
                        #endregion
                        // Lịch sử hàng hóa
                        StockHistoryDetail historyDetail = new StockHistoryDetail();
                        historyDetail.HistoryId = history.Id;
                        historyDetail.ProductId = item.ProductId;
                        historyDetail.Amount = item.Quantity;
                        historyDetail.ProductUnitId = item.ProductUnitId; // kg
                        historyDetail.Id = await svcStockHistoryDetail.Add(historyDetail);
                    }
                    issueDocket.Vat = orderVAT;
                    issueDocket.Amount = orderAmount;
                    issueDocket.TotalAmount = orderTotalAmount;
                    await svcStockIssueDocket.Modify(issueDocket);

                    transaction.Commit();
                    return issueDocket.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
        public async Task<int> CollectDeadstock(CollectDeadstockRequest _model)
        {
            if (_model.FishPondWarehouseId <= 0)
            {
                scopeContext.AddError("Chưa chọn ao");
                return 0;
            }
            if (_model.MassAmount <= 0)
            {
                scopeContext.AddError("Tổng trọng lượng không đúng");
                return 0;
            }
            if (_model.Ratio <= 0)
            {
                scopeContext.AddError("Tỷ lệ không đúng");
                return 0;
            }
            // dữ liệu ao
            var thisFishPond = await svcFishPond.GetByWarehouseId(_model.FishPondWarehouseId);
            if (thisFishPond == null)
            {
                scopeContext.AddError("Lỗi dữ liệu kho-ao " + _model.FishPondWarehouseId);
                return 0;
            }
            // đợt nuôi
            var thisFarmingSeason = await svcFarmingSeason.GetByFishPondId(thisFishPond.Id);
            if (thisFarmingSeason == null)
            {
                scopeContext.AddError("Ao này chưa vào đợt nuôi");
                return 0;
            }
            // dữ liệu kho-ao
            var thisFishPondWarehouse = await svcWarehouse.GetDetail(_model.FishPondWarehouseId);
            if (thisFishPondWarehouse == null || thisFishPondWarehouse.DefaultWarehouseId <= 0)
            {
                scopeContext.AddError("Lỗi dữ liệu kho mặc định cho ao");
                return 0;
            }
            _model.CollectDate = _model.CollectDate.GetValueOrDefault(DateTime.UtcNow);
            if (_model.DeadstockId <= 0)
            {
                scopeContext.AddError("Mã giống nuôi không tồn tại");
                return 0;
            }
            var deadstock = await svcProduct.GetDetail(_model.DeadstockId);
            if (deadstock == null)
            {
                scopeContext.AddError("Mã giống nuôi không tồn tại");
                return 0;
            }
            var lastFCR = await svcFeedConversionRate.GetLast(thisFarmingSeason.Id, _model.DeadstockId);
            if (lastFCR == null)
            {
                scopeContext.AddError("Ao không có đợt thả nào cho giống nuôi này");
                return 0;
            }
            if (lastFCR.SurveyDate.Date >= _model.CollectDate.Value.Date)
            {
                scopeContext.AddError($"Ngày {_model.CollectDate.Value.ToString("dd/MM/yyyy")} đã được kiểm tra cân trọng");
                return 0;
            }
            // bắt đầu tạo phiếu
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Lịch sử đợt nuôi (master lịch sử ao nuôi)
                    FarmingSeasonHistory history = new FarmingSeasonHistory();
                    history.ActionDate = _model.CollectDate.Value;
                    history.ActionType = (int)SystemIDEnum.FarmingSeason_ActionType_Collect_Deadstock;
                    history.Description = "Kiểm cá chết";
                    history.FarmingSeasonId = thisFarmingSeason.Id;
                    history.Id = await svcFarmingSeasonHistory.Add(history);

                    // Lịch sử con giống (detail lịch sử ao nuôi)
                    LivestockHistoryDetail historyDetail = new LivestockHistoryDetail();
                    historyDetail.HistoryId = history.Id;
                    historyDetail.LivestockId = _model.DeadstockId;
                    historyDetail.MassAmount = _model.MassAmount; // kg
                    historyDetail.DeadstockRatio = _model.Ratio; // Tỷ lệ dùng tính toán số lượng khi kiểm trọng
                    historyDetail.Id = await svcLivestockHistoryDetail.Add(historyDetail);
                    
                    transaction.Commit();
                    return history.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
        public async Task<int> FCRCheck(FCRCheckModel _model)
        {
            if (_model.FishPondWarehouseId <= 0)
            {
                scopeContext.AddError("Chưa chọn ao");
                return 0;
            }
            if (_model.Weight <= 0)
            {
                scopeContext.AddError("Trọng lượng không đúng");
                return 0;
            }
            // dữ liệu ao
            var thisFishPond = await svcFishPond.GetByWarehouseId(_model.FishPondWarehouseId);
            if (thisFishPond == null)
            {
                scopeContext.AddError("Lỗi dữ liệu kho-ao " + _model.FishPondWarehouseId);
                return 0;
            }
            // đợt nuôi
            var thisFarmingSeason = await svcFarmingSeason.GetByFishPondId(thisFishPond.Id);
            if (thisFarmingSeason == null)
            {
                scopeContext.AddError("Ao này chưa vào đợt nuôi");
                return 0;
            }
            // dữ liệu kho-ao
            var thisFishPondWarehouse = await svcWarehouse.GetDetail(_model.FishPondWarehouseId);
            if (thisFishPondWarehouse == null || thisFishPondWarehouse.DefaultWarehouseId <= 0)
            {
                scopeContext.AddError("Lỗi dữ liệu kho mặc định cho ao");
                return 0;
            }
            _model.CheckDate = _model.CheckDate.GetValueOrDefault(DateTime.UtcNow);
            if(_model.LivestockId <= 0)
            {
                scopeContext.AddError("Mã giống nuôi không tồn tại");
                return 0;
            }
            var livestock = await svcProduct.GetDetail(_model.LivestockId);
            if (livestock == null)
            {
                scopeContext.AddError("Mã giống nuôi không tồn tại");
                return 0;
            }
            var lastFCR = await svcFeedConversionRate.GetLast(thisFarmingSeason.Id, _model.LivestockId);
            if(lastFCR == null)
            {
                scopeContext.AddError("Ao không có đợt thả nào cho giống nuôi này");
                return 0;
            }
            if(lastFCR.SurveyDate.Date > _model.CheckDate.Value.Date)
            {
                scopeContext.AddError($"Ngày {_model.CheckDate.Value.ToString("dd/MM/yyyy")} đã được kiểm tra cân trọng" );
                return 0;
            }
            // tính hệ số tăng trọng:

            // bắt đầu tạo phiếu
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    //Tỷ lệ tăng trọng
                    FeedConversionRate fcr = new FeedConversionRate();
                    //fcr.FarmingSeasonId = thisFarmingSeason.Id;
                    //fcr.IsAuto = false;
                    //fcr.ProductId = _model.LivestockId;
                    //fcr.SurveyDate = _model.CheckDate.Value;
                    //fcr.Quantity = _model.Quantity;
                    //fcr.MassAmount = _model.MassAmount; // kg
                    //fcr.Weight = 1000 / (fcr.Quantity / fcr.MassAmount); // gram
                    //fcr.ProductName = "";
                    //fcr.Id = await svcFeedConversionRate.Add(fcr);

                    //// Lịch sử đợt nuôi (master lịch sử ao nuôi)
                    //FarmingSeasonHistory history = new FarmingSeasonHistory();
                    //history.ActionDate = _model.CheckDate.Value;
                    //history.ActionType = (int)SystemIDEnum.FarmingSeason_ActionType_FCR;
                    //history.Description = "Kiểm tra tăng trọng";
                    //history.FarmingSeasonId = thisFarmingSeason.Id;
                    //history.Id = await svcFarmingSeasonHistory.Add(history);

                    //// Lịch sử con giống (detail lịch sử ao nuôi)
                    //LivestockHistoryDetail historyDetail = new LivestockHistoryDetail();
                    //historyDetail.HistoryId = history.Id;
                    //historyDetail.LivestockId = _model.LivestockId;
                    //historyDetail.MassAmount = _model.MassAmount; // kg
                    //historyDetail.Quantity = _model.Quantity;
                    //historyDetail.Weight = fcr.Weight; // gram
                    //historyDetail.Id = await svcLivestockHistoryDetail.Add(historyDetail);

                    //transaction.Commit();
                    return fcr.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }
}

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
    public class StockReceiveDocketBusiness : IStockReceiveDocketBusiness
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        private readonly IStockReceiveDocketService svcStockReceiveDocket;
        private readonly IStockReceiveDocketDetailService svcStockReceiveDocketDetail;
        private readonly IStockReceiveDocketTypeService svcStockReceiveDocketType;
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
        private readonly IMapper iMapper;
        public StockReceiveDocketBusiness(
            FLSDbContext _context,
            IScopeContext _scopeContext,
            IStockReceiveDocketService _svcStockReceiveDocket,
            IStockReceiveDocketDetailService _svcStockReceiveDocketDetail,
            IStockReceiveDocketTypeService _svcStockReceiveDocketType,
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
            IMapper _iMapper)
        {
            context = _context;
            scopeContext = _scopeContext;
            svcStockReceiveDocket = _svcStockReceiveDocket;
            svcStockReceiveDocketDetail = _svcStockReceiveDocketDetail;
            svcStockReceiveDocketType = _svcStockReceiveDocketType;
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
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockReceiveDocketModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<StockReceiveDocketModel>>(await svcStockReceiveDocket.GetList(_model));
        }
        public async Task<ImportStockDetailModel> GetDetail(int _id)
        {
            ImportStockDetailModel result = new ImportStockDetailModel();
            StockReceiveDocketModel receiveDocket = iMapper.Map<StockReceiveDocketModel>(await svcStockReceiveDocket.GetDetail(_id));
            if(receiveDocket == null)
            {
                scopeContext.AddError("Mã phiếu nhập không tồn tại");
                return null;
            }
            result.ReceiveDocket = receiveDocket;
            List<StockReceiveDocketDetailModel> details = iMapper.Map<List<StockReceiveDocketDetailModel>>(await svcStockReceiveDocketDetail.GetDetailsByDocketId(_id));
            result.Details = details;
            return result;
        }
        public async Task<int> Add(ImportStockModel _model)
        {
            if(_model == null || _model.ReceiveDocket == null || _model.Suppliers == null)
            {
                scopeContext.AddError("Lỗi dữ liệu đầu vào");
                return 0;
            }
            if(_model.ReceiveDocket.StockReceiveDocketTypeId <= 0)
            {
                scopeContext.AddError("Chưa chọn loại phiếu nhập");
                return 0;
            }
            if (_model.ReceiveDocket.WarehouseId <= 0)
            {
                scopeContext.AddError("Chưa chọn kho nhập");
                return 0;
            }
            // lấy loại phiếu chi
            var receiveDocketType = await svcStockReceiveDocketType.GetDetail(_model.ReceiveDocket.StockReceiveDocketTypeId);
            var paySlipType = 0;
            if (receiveDocketType != null && receiveDocketType.PayslipNeeded)
                paySlipType = receiveDocketType.PayslipTypeId;
            // bắt đầu tạo phiếu
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    StockReceiveDocket docket = iMapper.Map<StockReceiveDocket>(_model.ReceiveDocket);
                    docket.ExecutorCode = scopeContext.UserCode;
                    docket.ExecutedDate = DateTime.UtcNow;
                    if (docket.IsActuallyReceived.GetValueOrDefault(false))
                    {
                        docket.ActuallyReceivedDate = docket.ExecutedDate;
                        docket.ActuallyReceivedCode = docket.ExecutorCode;
                    }
                    docket.Id = await svcStockReceiveDocket.Add(docket);
                    decimal orderVAT = 0;
                    decimal orderAmount = 0;
                    decimal orderTotalAmount = 0;
                    List<ProductInstockModel> productInstock = new List<ProductInstockModel>();
                    foreach (ImportStockSupplierModel item in _model.Suppliers)
                    {
                        ExpenditureDocket exp = null;
                        if (paySlipType > 0)
                        {
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
                            exp.Amount = 0;
                            exp.TotalAmount = 0;
                            exp.Vat = 0;
                            exp.Id = await svcExpenditureDocket.Add(exp);
                        }
                        decimal supplierAmount = 0;
                        decimal supplierTotalAmount = 0;
                        decimal supplierVat = 0;
                        foreach (StockReceiveDocketDetailModel i in item.ReceiveDocketDetails)
                        {
                            StockReceiveDocketDetail docketDetail = iMapper.Map<StockReceiveDocketDetail>(i);
                            docketDetail.StockReceiveDocketId = docket.Id;
                            docketDetail.SupplierBranchId = item.SupplierBranchId;
                            docketDetail.SupplierBranchName = item.SupplierBranchName;
                            if (paySlipType > 0)
                            {
                                docketDetail.Amount = i.Quantity * i.UnitPrice;
                                docketDetail.Vat = docketDetail.Amount * (i.VatPercent / (decimal)100);
                                docketDetail.TotalAmount = docketDetail.Amount + docketDetail.Vat;
                            }
                            else
                            {
                                docketDetail.UnitPrice = 0;
                                docketDetail.Amount = 0;
                                docketDetail.Vat = 0;
                                docketDetail.TotalAmount = 0;
                            }
                            docketDetail.Id = await svcStockReceiveDocketDetail.Add(docketDetail);
                            if (paySlipType > 0)
                            {
                                ExpenditureDocketDetail eD = new ExpenditureDocketDetail();
                                eD.ExpenditureDocketId = exp.Id;
                                eD.VatPercent = docketDetail.VatPercent;
                                eD.Amount = docketDetail.Amount;
                                eD.Vat = docketDetail.Vat;
                                eD.TotalAmount = docketDetail.TotalAmount;
                                eD.ProductId = docketDetail.ProductId;
                                eD.ExpenditureTypeId = paySlipType;
                                eD.Id = await svcExpenditureDocketDetail.Add(eD);
                            }
                            supplierAmount += docketDetail.Amount;
                            supplierTotalAmount += docketDetail.TotalAmount;
                            supplierVat += docketDetail.Vat;

                            #region Thêm vào danh sách tồn - Tạm thời chưa chuyển đổi sang số lượng theo đơn vị tính chuẩn
                            var idx = productInstock.FindIndex(p => p.ProductId == i.ProductId);
                            if (idx >= 0)
                                productInstock[idx].Quantity += i.Quantity;
                            else
                            {
                                productInstock.Add(new ProductInstockModel()
                                {
                                    ProductId = i.ProductId,
                                    ProductName = i.ProductName,
                                    Quantity = i.Quantity,
                                    ProductUnitId = i.ProductUnitId
                                });
                            }
                            #endregion
                        }
                        if(paySlipType > 0)
                        {
                            exp.Amount += supplierAmount;
                            exp.TotalAmount += supplierTotalAmount;
                            exp.Vat += supplierVat;
                            await svcExpenditureDocket.Modify(exp);
                        }
                        orderVAT += supplierVat;
                        orderAmount += supplierAmount;
                        orderTotalAmount += supplierTotalAmount;
                    }
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

                    #region Cập nhật vào bảng tồn
                    foreach(var current in productInstock)
                    {
                        var instock = await svcCurrentInStock.GetList(docket.WarehouseId, current.ProductId);
                        if(instock == null || instock.Count == 0)
                        {
                            CurrentInStock cis = new CurrentInStock()
                            {
                                Amount = docket.IsActuallyReceived == true ? current.Quantity : 0,
                                AmountExpect = current.Quantity,
                                ProductId = current.ProductId,
                                ProductUnitId = current.ProductUnitId,
                                WarehouseId = docket.WarehouseId
                            };
                            cis.Id = await svcCurrentInStock.Add(cis);
                        }
                        else
                        {
                            CurrentInStock cis = instock[0];
                            if (docket.IsActuallyReceived == true)
                                cis.Amount += current.Quantity;
                            cis.AmountExpect += current.Quantity;
                            await svcCurrentInStock.Modify(cis);
                        }
                    }
                    #endregion

                    transaction.Commit();
                    return docket.Id;
                }catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
        public async Task<bool> Modify(int _id, StockReceiveDocketModel _model)
        {
            StockReceiveDocket entity = await svcStockReceiveDocket.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcStockReceiveDocket.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcStockReceiveDocket.Remove(_id);
        }
        public async Task<int> ReleaseLivestock(ReleaseLivestockModel _model)
        {
            if (_model == null || _model.LivestockDocket == null || _model.Suppliers == null)
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
            if(thisFishPond == null)
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
                    List<ProductInstockModel> productInstock = new List<ProductInstockModel>();
                    foreach (ImportStockSupplierModel item in _model.Suppliers)
                    {
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
                        exp.Amount = 0;
                        exp.TotalAmount = 0;
                        exp.Vat = 0;
                        exp.Id = await svcExpenditureDocket.Add(exp);
                        
                        decimal supplierAmount = 0;
                        decimal supplierTotalAmount = 0;
                        decimal supplierVat = 0;
                        foreach (StockReceiveDocketDetailModel i in item.ReceiveDocketDetails)
                        {
                            // Chi tiết phiếu nhập
                            StockReceiveDocketDetail docketDetail = iMapper.Map<StockReceiveDocketDetail>(i);
                            docketDetail.StockReceiveDocketId = docket.Id;
                            docketDetail.SupplierBranchId = item.SupplierBranchId;
                            docketDetail.SupplierBranchName = item.SupplierBranchName;
                            docketDetail.Amount = i.Quantity * i.UnitPrice;
                            docketDetail.Vat = docketDetail.Amount * (i.VatPercent / (decimal)100);
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

                            supplierAmount += docketDetail.Amount;
                            supplierTotalAmount += docketDetail.TotalAmount;
                            supplierVat += docketDetail.Vat;

                            //Tỷ lệ tăng trọng
                            FeedConversionRate fcr = new FeedConversionRate();
                            fcr.FarmingSeasonId = thisFarmingSeason.Id;
                            fcr.IsAuto = true;
                            fcr.ProductId = docketDetail.ProductId;
                            fcr.SurveyDate = _model.LivestockDocket.ReceiveDate.GetValueOrDefault(DateTime.UtcNow);
                            fcr.Quantity = i.LivestockQuantity;
                            fcr.MassAmount = i.Quantity; // kg
                            fcr.Weight = 1000 / (i.LivestockQuantity/ i.Quantity); // gram
                            fcr.ProductName = "";
                            fcr.Id = await svcFeedConversionRate.Add(fcr);

                            // Lịch sử con giống (detail lịch sử ao nuôi)
                            LivestockHistoryDetail historyDetail = new LivestockHistoryDetail();
                            historyDetail.HistoryId = history.Id;
                            historyDetail.LivestockId = docketDetail.ProductId;
                            historyDetail.LivestockSize = i.LivestockSize;
                            historyDetail.MassAmount = i.Quantity; // kg
                            historyDetail.Quantity = i.LivestockQuantity;
                            historyDetail.Weight = fcr.Weight; // gram
                            historyDetail.Id = await svcLivestockHistoryDetail.Add(historyDetail);
                        }
                        exp.Amount += supplierAmount;
                        exp.TotalAmount += supplierTotalAmount;
                        exp.Vat += supplierVat;
                        await svcExpenditureDocket.Modify(exp);
                        
                        orderVAT += supplierVat;
                        orderAmount += supplierAmount;
                        orderTotalAmount += supplierTotalAmount;
                    }
                    // cập nhật phiếu xuất
                    issueDocket.Vat = orderVAT;
                    issueDocket.Amount = orderAmount;
                    issueDocket.TotalAmount = orderTotalAmount;
                    await svcStockIssueDocket.Modify(issueDocket);

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
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

    }
}

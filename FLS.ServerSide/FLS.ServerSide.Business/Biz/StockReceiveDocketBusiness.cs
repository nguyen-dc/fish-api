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
        private readonly IExpenditureDocketService svcExpenditureDocket;
        private readonly IExpenditureDocketDetailService svcExpenditureDocketDetail;
        private readonly ICurrentInStockService svcCurrentInStock;
        private readonly IMapper iMapper;
        public StockReceiveDocketBusiness(
            FLSDbContext _context,
            IScopeContext _scopeContext,
            IStockReceiveDocketService _svcStockReceiveDocket,
            IStockReceiveDocketDetailService _svcStockReceiveDocketDetail,
            IExpenditureDocketService _svcExpenditureDocket,
            IExpenditureDocketDetailService _svcExpenditureDocketDetail,
            ICurrentInStockService _svcCurrentInStock,
            IMapper _iMapper)
        {
            context = _context;
            scopeContext = _scopeContext;
            svcStockReceiveDocket = _svcStockReceiveDocket;
            svcStockReceiveDocketDetail = _svcStockReceiveDocketDetail;
            svcExpenditureDocket = _svcExpenditureDocket;
            svcExpenditureDocketDetail = _svcExpenditureDocketDetail;
            svcCurrentInStock = _svcCurrentInStock;
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
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    StockReceiveDocket docket = iMapper.Map<StockReceiveDocket>(_model.ReceiveDocket);
                    docket.ExecutorCode = "admin";
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
                        ExpenditureDocket exp = new ExpenditureDocket();
                        exp.StockDocketId = docket.Id;
                        exp.PartnerId = item.SupplierBranchId;
                        exp.PartnerName = item.SupplierBranchName;
                        exp.WarehouseId = docket.WarehouseId;
                        exp.BillCode = item.BillCode;
                        exp.BillSerial = item.BillSerial;
                        exp.BillTemplateCode = item.BillTemplateCode;
                        exp.BillDate = item.BillDate;
                        exp.CreatedUser = "admin";
                        exp.ExpendDate = docket.ExecutedDate;
                        exp.Amount = 0;
                        exp.TotalAmount = 0;
                        exp.Vat = 0;
                        exp.Id = await svcExpenditureDocket.Add(exp);

                        foreach (StockReceiveDocketDetailModel i in item.ReceiveDocketDetails)
                        {
                            StockReceiveDocketDetail docketDetail = iMapper.Map<StockReceiveDocketDetail>(i);
                            docketDetail.StockReceiveDocketId = docket.Id;
                            docketDetail.SupplierBranchId = item.SupplierBranchId;
                            docketDetail.SupplierBranchName = item.SupplierBranchName;
                            docketDetail.Amount = i.Quantity * i.UnitPrice;
                            docketDetail.Vat = docketDetail.Amount * (i.VatPercent / (decimal)100);
                            docketDetail.TotalAmount = docketDetail.Amount + docketDetail.Vat;
                            docketDetail.Id = await svcStockReceiveDocketDetail.Add(docketDetail);

                            ExpenditureDocketDetail eD = new ExpenditureDocketDetail();
                            eD.ExpenditureDocketId = exp.Id;
                            eD.VatPercent = docketDetail.VatPercent;
                            eD.Amount = docketDetail.Amount;
                            eD.Vat = docketDetail.Vat;
                            eD.TotalAmount = docketDetail.TotalAmount;
                            eD.ProductId = docketDetail.ProductId;
                            eD.ExpenditureTypeId = i.ReceiptTypeId;
                            eD.Id = await svcExpenditureDocketDetail.Add(eD);

                            exp.Amount += docketDetail.Amount;
                            exp.TotalAmount += docketDetail.TotalAmount;
                            exp.Vat += docketDetail.Vat;

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
                        await svcExpenditureDocket.Modify(exp);
                        orderVAT += exp.Vat;
                        orderAmount += exp.Amount;
                        orderTotalAmount += exp.TotalAmount;
                    }

                    ExpenditureDocket expendDocket = new ExpenditureDocket();
                    expendDocket.StockDocketId = docket.Id;
                    expendDocket.WarehouseId = docket.WarehouseId;
                    expendDocket.CreatedUser = "admin";
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
        public async Task<int> ReleaseLivestock(ImportStockModel _model)
        {
            if (_model == null || _model.ReceiveDocket == null || _model.Suppliers == null)
            {
                scopeContext.AddError("Lỗi dữ liệu đầu vào");
                return 0;
            }
            if (_model.ReceiveDocket.StockReceiveDocketTypeId <= 0)
            {
                scopeContext.AddError("Chưa chọn loại phiếu nhập");
                return 0;
            }
            // WarehouseId: mã kho-ao
            if (_model.ReceiveDocket.WarehouseId <= 0)
            {
                scopeContext.AddError("Chưa chọn ao thả");
                return 0;
            }
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    StockReceiveDocket docket = iMapper.Map<StockReceiveDocket>(_model.ReceiveDocket);
                    docket.ExecutorCode = "admin";
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
                    foreach (ImportStockSupplierModel item in _model.Suppliers)
                    {
                        ExpenditureDocket exp = new ExpenditureDocket();
                        exp.StockDocketId = docket.Id;
                        exp.PartnerId = item.SupplierBranchId;
                        exp.PartnerName = item.SupplierBranchName;
                        exp.WarehouseId = docket.WarehouseId;
                        exp.BillCode = item.BillCode;
                        exp.BillSerial = item.BillSerial;
                        exp.BillTemplateCode = item.BillTemplateCode;
                        exp.BillDate = item.BillDate;
                        exp.CreatedUser = "admin";
                        exp.ExpendDate = docket.ExecutedDate;
                        exp.Amount = 0;
                        exp.TotalAmount = 0;
                        exp.Vat = 0;
                        exp.Id = await svcExpenditureDocket.Add(exp);

                        foreach (StockReceiveDocketDetailModel i in item.ReceiveDocketDetails)
                        {
                            StockReceiveDocketDetail docketDetail = iMapper.Map<StockReceiveDocketDetail>(i);
                            docketDetail.StockReceiveDocketId = docket.Id;
                            docketDetail.SupplierBranchId = item.SupplierBranchId;
                            docketDetail.SupplierBranchName = item.SupplierBranchName;
                            docketDetail.Amount = i.Quantity * i.UnitPrice;
                            docketDetail.Vat = docketDetail.Amount * (i.VatPercent / (decimal)100);
                            docketDetail.TotalAmount = docketDetail.Amount + docketDetail.Vat;
                            docketDetail.Id = await svcStockReceiveDocketDetail.Add(docketDetail);

                            ExpenditureDocketDetail eD = new ExpenditureDocketDetail();
                            eD.ExpenditureDocketId = exp.Id;
                            eD.VatPercent = docketDetail.VatPercent;
                            eD.Amount = docketDetail.Amount;
                            eD.Vat = docketDetail.Vat;
                            eD.TotalAmount = docketDetail.TotalAmount;
                            eD.ProductId = docketDetail.ProductId;
                            eD.ExpenditureTypeId = i.ReceiptTypeId;
                            eD.Id = await svcExpenditureDocketDetail.Add(eD);

                            exp.Amount += docketDetail.Amount;
                            exp.TotalAmount += docketDetail.TotalAmount;
                            exp.Vat += docketDetail.Vat;
                        }
                        await svcExpenditureDocket.Modify(exp);
                        orderVAT += exp.Vat;
                        orderAmount += exp.Amount;
                        orderTotalAmount += exp.TotalAmount;
                    }

                    ExpenditureDocket expendDocket = new ExpenditureDocket();
                    expendDocket.StockDocketId = docket.Id;
                    expendDocket.WarehouseId = docket.WarehouseId;
                    expendDocket.CreatedUser = "admin";
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

    }
}

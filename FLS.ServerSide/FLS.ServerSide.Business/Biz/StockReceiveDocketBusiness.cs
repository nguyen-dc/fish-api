using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class StockReceiveDocketBusiness : IStockReceiveDocketBusiness
    {
        private static FLSDbContext context;
        private readonly IStockReceiveDocketService svcStockReceiveDocket;
        private readonly IStockReceiveDocketDetailService svcStockReceiveDocketDetail;
        private readonly IExpenditureDocketService svcExpenditureDocket;
        private readonly IExpenditureDocketDetailService svcExpenditureDocketDetail;
        private readonly IMapper iMapper;
        public StockReceiveDocketBusiness(
            FLSDbContext _context,
            IStockReceiveDocketService _svcStockReceiveDocket,
            IStockReceiveDocketDetailService _svcStockReceiveDocketDetail,
            IExpenditureDocketService _svcExpenditureDocket,
            IExpenditureDocketDetailService _svcExpenditureDocketDetail,
            IMapper _iMapper)
        {
            context = _context;
            svcStockReceiveDocket = _svcStockReceiveDocket;
            svcStockReceiveDocketDetail = _svcStockReceiveDocketDetail;
            svcExpenditureDocket = _svcExpenditureDocket;
            svcExpenditureDocketDetail = _svcExpenditureDocketDetail;
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockReceiveDocketModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<StockReceiveDocketModel>>(await svcStockReceiveDocket.GetList(_model));
        }
        public async Task<ImportStockModel> GetDetail(int _id)
        {
            ImportStockModel result = new ImportStockModel();
            StockReceiveDocketModel receiveDocket = iMapper.Map<StockReceiveDocketModel>(await svcStockReceiveDocket.GetDetail(_id));
            result.ReceiveDocket = receiveDocket;
            return result;
        }
        public async Task<int> Add(ImportStockModel _model)
        {
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
                            docketDetail.Amount = i.Quantity * i.UnitPrice;
                            docketDetail.Vat = i.Amount * (i.VatPercent / 100);
                            docketDetail.TotalAmount = i.Amount + i.Vat;
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
    }
}

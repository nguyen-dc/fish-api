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
        private readonly IStockReceiveDocketService svcStockReceiveDocket;
        //private readonly IStockReceiveDocketServiceDetail svcStockReceiveDocketDetail;
        //private readonly IExpenditureDocketService svcExpenditureDocket;
        //private readonly IExpenditureDocketDetailService svcExpenditureDocketDetail;
        private readonly IMapper iMapper;
        public StockReceiveDocketBusiness(IStockReceiveDocketService _svcStockReceiveDocket, IMapper _iMapper)
        {
            svcStockReceiveDocket = _svcStockReceiveDocket;
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
            StockReceiveDocket docket = iMapper.Map<StockReceiveDocket>(_model.ReceiveDocket);
            docket.Id = await svcStockReceiveDocket.Add(docket);

            decimal totalVAT = 0;
            decimal totalAmount = 0;
            foreach (ImportStockSupplierModel item in _model.Suppliers)
            {
                ExpenditureDocket exp = new ExpenditureDocket();
                exp.PartnerId = item.SupplierBranchId;
                //exp.PartnerName
                exp.StockDocketId = docket.Id;
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
                //exp.UnpaidAmount = 0;
                //exp.Id = await svcExpenditureDocket.Add(exp, false);

                foreach (StockReceiveDocketDetailModel i in item.ReceiveDocketDetails)
                {
                    StockReceiveDocketDetail docketDetail = iMapper.Map<StockReceiveDocketDetail>(i);
                    docketDetail.StockReceiveDocketId = docket.Id;
                    docketDetail.Amount = docketDetail.UnitPrice * docketDetail.Quantity;
                    docketDetail.Vat = docketDetail.Amount * (docketDetail.VatPercent / 100);
                    docketDetail.TotalAmount = docketDetail.Amount + docketDetail.Vat;
                    //docketDetail.Id = await svcStockReceiveDocketDetail.Add(docketDetail, false);

                    ExpenditureDocketDetail eD = new ExpenditureDocketDetail();
                    eD.Amount = docketDetail.Amount;
                    eD.CreatedUser = "admin";
                    eD.ExpenditureDocketId = exp.Id;
                    eD.ProductId = docketDetail.ProductId;
                    eD.TotalAmount = docketDetail.TotalAmount;
                    eD.Vat = docketDetail.Vat;
                    eD.VatPercent = docketDetail.VatPercent;
                    //await svcExpenditureDocketDetail.Add(eD, false);

                    exp.Amount += docketDetail.TotalAmount;
                    exp.Vat += docketDetail.Vat;
                    totalVAT += docketDetail.Vat;
                }
                exp.TotalAmount = exp.Amount;
                totalAmount += exp.TotalAmount;
            }

            ExpenditureDocket expendDocket = new ExpenditureDocket();
            expendDocket.StockDocketId = docket.Id;
            expendDocket.WarehouseId = docket.WarehouseId;
            expendDocket.CreatedUser = "admin";
            expendDocket.ExpendDate = docket.ExecutedDate;
            expendDocket.Amount = 0;
            expendDocket.TotalAmount = 0;
            expendDocket.Vat = 0;
            //expendDocket.UnpaidAmount = 0;
            //expendDocket.Id = await svcExpenditureDocket.Add(expendDocket, false);

            foreach (ExpenditureDocketDetailModel item in _model.PaySlipDetails)
            {
                ExpenditureDocketDetail eD = iMapper.Map< ExpenditureDocketDetail >(item);
                eD.CreatedUser = "admin";
                eD.ExpenditureDocketId = expendDocket.Id;
                //await svcExpenditureDocketDetail.Add(eD, false);

                expendDocket.Amount += eD.TotalAmount;
                expendDocket.Vat += eD.Vat;
                totalVAT += eD.Vat;
            }
            expendDocket.TotalAmount = expendDocket.Amount;
            totalAmount += expendDocket.TotalAmount;

            // cập nhật phiếu nhập
            docket.Vat = totalVAT;
            docket.Amount = totalAmount;
            docket.TotalAmount = totalAmount + totalVAT;
            await svcStockReceiveDocket.Modify(docket);
            return docket.Id;
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

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
            //return iMapper.Map<ImportStockModel>(await svcStockReceiveDocket.GetDetail(_id));
            return result;
        }
        public async Task<int> Add(ImportStockModel _model)
        {
            StockReceiveDocket docket = iMapper.Map<StockReceiveDocket>(_model.ReceiveDocket);
            List<StockReceiveDocketDetail> docketDetails = iMapper.Map<List<StockReceiveDocketDetail>>(_model.ReceiveDocketDetails);
            List<ExpenditureDocketDetail> expendDetails = iMapper.Map<List<ExpenditureDocketDetail>>(_model.PaySlipDetails);
            // cập nhật chi tiết phiếu nhập
            decimal totalVAT = 0;
            decimal totalAmount = 0;
            //foreach(var model in docketDetails)
            //{
            //    model.Amount = model.UnitPrice * model.Quantity;
            //    model.Vat = model.Amount * (model.VatPercent / 100);
            //    model.TotalAmount = model.Amount + model.Vat;
            //    totalVAT += model.Vat;
            //    totalAmount += model.Amount;
            //}
            //// cập nhật phiếu nhập
            //docket.Vat = totalVAT;
            //docket.Amount = totalAmount;
            //docket.TotalAmount = totalAmount + totalVAT;

            //// cập nhật chi tiết phiếu chi
            //foreach (var model in expendDetails)
            //{
            //    model.Amount = model.UnitPrice * model.Quantity;
            //    model.Vat = model.Amount * (model.VatPercent / 100);
            //    model.TotalAmount = model.Amount + model.Vat;
            //    totalVAT += model.Vat;
            //    totalAmount += model.Amount;
            //}
                // cập nhật phiếu chi

                ExpenditureDocket expend = new ExpenditureDocket();
            //expend.
            //return await svcStockReceiveDocket.Add(entity);
            return 0;
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

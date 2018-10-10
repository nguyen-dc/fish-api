using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.Model.Scope;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class StockIssueDocketBusiness : IStockIssueDocketBusiness
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        private readonly IStockIssueDocketService svcStockIssueDocket;
        private readonly IStockIssueDocketDetailService svcStockIssueDocketDetail;
        private readonly IExpenditureDocketService svcExpenditureDocket;
        private readonly IExpenditureDocketDetailService svcExpenditureDocketDetail;
        private readonly IMapper iMapper;
        public StockIssueDocketBusiness(
            FLSDbContext _context,
            IScopeContext _scopeContext,
            IStockIssueDocketService _svcStockIssueDocket,
            IStockIssueDocketDetailService _svcStockIssueDocketDetail,
            IExpenditureDocketService _svcExpenditureDocket,
            IExpenditureDocketDetailService _svcExpenditureDocketDetail,
            IMapper _iMapper)
        {
            context = _context;
            scopeContext = _scopeContext;
            svcStockIssueDocket = _svcStockIssueDocket;
            svcStockIssueDocketDetail = _svcStockIssueDocketDetail;
            svcExpenditureDocket = _svcExpenditureDocket;
            svcExpenditureDocketDetail = _svcExpenditureDocketDetail;
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockIssueDocketModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<StockIssueDocketModel>>(await svcStockIssueDocket.GetList(_model));
        }
        public async Task<ExportStockDetailModel> GetDetail(int _id)
        {
            ExportStockDetailModel result = new ExportStockDetailModel();
            StockIssueDocketModel issueDocket = iMapper.Map<StockIssueDocketModel>(await svcStockIssueDocket.GetDetail(_id));
            if (issueDocket == null)
            {
                scopeContext.AddError("Mã phiếu xuất không tồn tại");
                return null;
            }
            result.IssueDocket = issueDocket;
            List<StockIssueDocketDetailModel> details = iMapper.Map<List<StockIssueDocketDetailModel>>(await svcStockIssueDocketDetail.GetDetailsByDocketId(_id));
            result.Details = details;
            return result;
        }
        public async Task<int> Add(ExportStockModel _model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                StockIssueDocket issueDocket = iMapper.Map<StockIssueDocket>(_model.IssueDocket);
                issueDocket.CustomerId = _model.Receipt.PartnerId;
                issueDocket.CustomerName = _model.Receipt.PartnerName;
                ExpenditureDocket receipt = iMapper.Map<ExpenditureDocket>(_model.Receipt);
                receipt.Amount = issueDocket.Amount;
                receipt.Vat = issueDocket.Vat;
                receipt.WarehouseId = issueDocket.WarehouseId;
                receipt.CreatedUser = "admin";

                List<StockIssueDocketDetail> docketDetails = iMapper.Map<List<StockIssueDocketDetail>>(_model.DocketDetails);
                List<ExpenditureDocketDetail> expendDetails = new List<ExpenditureDocketDetail>();
                decimal orderVAT = 0;
                decimal orderAmount = 0;
                decimal orderTotalAmount = 0;
                foreach (var item in docketDetails)
                {
                    item.Amount = item.Quantity * item.UnitPrice;
                    item.Vat = item.Amount * (item.VatPercent / 100);
                    item.TotalAmount = item.Amount + item.Vat;

                    ExpenditureDocketDetail exDetail = new ExpenditureDocketDetail
                    {
                        ExpenditureDocketId = receipt.Id,
                        Amount = item.Amount,
                        Vat = item.Vat,
                        TotalAmount = item.TotalAmount
                    };
                    expendDetails.Add(exDetail);

                    orderVAT += item.Vat;
                    orderAmount += item.Amount;
                    orderTotalAmount += item.TotalAmount;
                }
                issueDocket.Vat = orderVAT;
                issueDocket.Amount = orderAmount;
                issueDocket.TotalAmount = orderTotalAmount;
                receipt.Vat = orderVAT;
                receipt.Amount = orderAmount;
                receipt.TotalAmount = orderTotalAmount;
                // insert
                issueDocket.Id = await svcStockIssueDocket.Add(issueDocket);
                receipt.StockDocketId = issueDocket.Id;
                receipt.Id = await svcExpenditureDocket.Add(receipt);

                foreach (var item in docketDetails)
                {
                    item.StockIssueDocketId = issueDocket.Id;
                    item.Id = await svcStockIssueDocketDetail.Add(item);
                }
                foreach (var item in expendDetails)
                {
                    item.ExpenditureDocketId = receipt.Id;
                    item.Id = await svcExpenditureDocketDetail.Add(item);
                }
                transaction.Commit();
                return issueDocket.Id;
            }
        }
        public async Task<bool> Modify(int _id, StockIssueDocketModel _model)
        {
            StockIssueDocket entity = await svcStockIssueDocket.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcStockIssueDocket.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcStockIssueDocket.Remove(_id);
        }
    }
}

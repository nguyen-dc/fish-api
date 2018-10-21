using FLS.ServerSide.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FLS.ServerSide.SharingObject;
using FLS.ServerSide.Model.Scope;
using AutoMapper;

namespace FLS.ServerSide.EFCore.Services
{
    public class StockIssueDocketDetailService : EFCoreServiceBase, IStockIssueDocketDetailService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        private readonly IMapper iMapper;
        public StockIssueDocketDetailService(
            FLSDbContext _context, 
            IScopeContext _scopeContext,
            IMapper _iMapper) : base(_context, _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockIssueDocketDetail>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();

            var items = await context.StockIssueDocketDetail.Where(i => 
                        i.IsDeleted == false
                       // &&(_model.Key == null || i..Contains(_model.Key))
                    ).OrderByDescending(i => i.UpdatedDate.HasValue ? i.UpdatedDate : i.CreatedDate).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<StockIssueDocketDetail> GetDetail(int _id)
        {
            var item = await context.StockIssueDocketDetail.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<List<StockIssueDocketDetailModel>> GetDetailsByDocketId(int _docketId)
        {
            var __params = new
            {
                docketId = _docketId
            };
            var details = await CallStored<StockIssueDocketDetailModel>("SP_Export_Get_Details", __params).ToListAsync();
            return details;
        }
        public async Task<int> Add(StockIssueDocketDetail _model)
        {
            _model.CreatedUser = scopeContext.UserCode;
            _model.CreatedDate = DateTime.Now;
            context.StockIssueDocketDetail.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(StockIssueDocketDetail _model)
        {
            _model.UpdatedUser = scopeContext.UserCode;
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id)
        {
            StockIssueDocketDetail item = await context.StockIssueDocketDetail.Where(i => i.Id == _id).FirstOrDefaultAsync();
            if (item == default(StockIssueDocketDetail)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            await context.SaveChangesAsync();
            return true;
        }
    }
}

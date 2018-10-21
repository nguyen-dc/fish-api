using FLS.ServerSide.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FLS.ServerSide.SharingObject;
using FLS.ServerSide.Model.Scope;
using AutoMapper;
using MySql.Data.MySqlClient;

namespace FLS.ServerSide.EFCore.Services
{
    public class StockReceiveDocketDetailService : EFCoreServiceBase, IStockReceiveDocketDetailService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        private readonly IMapper iMapper;
        public StockReceiveDocketDetailService(
            FLSDbContext _context, 
            IScopeContext _scopeContext,
            IMapper _iMapper) : base(_context, _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockReceiveDocketDetail>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();

            var items = await context.StockReceiveDocketDetail.Where(i => 
                        i.IsDeleted == false
                       // &&(_model.Key == null || i..Contains(_model.Key))
                    ).OrderByDescending(i => i.UpdatedDate.HasValue ? i.UpdatedDate : i.CreatedDate).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<StockReceiveDocketDetail> GetDetail(int _id)
        {
            var item = await context.StockReceiveDocketDetail.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<List<StockReceiveDocketDetailModel>> GetDetailsByDocketId(int _docketId)
        {
            var __params = new {
                docketId = _docketId
            };
            var details = await CallStored<StockReceiveDocketDetailModel>("SP_Import_Get_Details", __params).ToListAsync();
            return details;
        }
        public async Task<int> Add(StockReceiveDocketDetail _model)
        {
            _model.CreatedUser = scopeContext.UserCode;
            _model.CreatedDate = DateTime.Now;
            context.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(StockReceiveDocketDetail _model)
        {
            _model.UpdatedUser = scopeContext.UserCode;
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id)
        {
            StockReceiveDocketDetail item = await context.StockReceiveDocketDetail.Where(i => i.Id == _id).FirstOrDefaultAsync();
            if (item == default(StockReceiveDocketDetail)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            await context.SaveChangesAsync();
            return true;
        }
    }
}

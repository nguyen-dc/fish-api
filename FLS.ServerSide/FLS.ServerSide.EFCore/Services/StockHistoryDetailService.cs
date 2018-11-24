using FLS.ServerSide.EFCore.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FLS.ServerSide.Model.Scope;

namespace FLS.ServerSide.EFCore.Services
{
    public class StockHistoryDetailService : IStockHistoryDetailService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        public StockHistoryDetailService(FLSDbContext _context, IScopeContext _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
        }
        public async Task<StockHistoryDetail> GetDetail(int _id)
        {
            var item = await context.StockHistoryDetail.FirstOrDefaultAsync(x => x.Id == _id);
            return item;
        }
        public async Task<int> Add(StockHistoryDetail _model)
        {
            _model.CreatedUser = scopeContext.UserCode;
            _model.CreatedDate = DateTime.Now;
            context.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(StockHistoryDetail _model)
        {
            _model.UpdatedUser = scopeContext.UserCode;
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

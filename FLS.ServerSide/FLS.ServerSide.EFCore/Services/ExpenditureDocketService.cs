using FLS.ServerSide.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FLS.ServerSide.SharingObject;
using FLS.ServerSide.Model.Scope;

namespace FLS.ServerSide.EFCore.Services
{
    public class ExpenditureDocketService : IExpenditureDocketService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        public ExpenditureDocketService(FLSDbContext _context, IScopeContext _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
        }
        public async Task<PagedList<ExpenditureDocket>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            var items = await context.ExpenditureDocket.Where(i => 
                        i.IsDeleted == false
                        &&(_model.Key == null || i.PartnerName.Contains(_model.Key))
                    ).OrderByDescending(i => i.UpdatedDate.HasValue ? i.UpdatedDate : i.CreatedDate).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<ExpenditureDocket> GetDetail(int _id)
        {
            var item = await context.ExpenditureDocket.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<List<ExpenditureDocket>> GetByStockDocketId(int stockDocketId)
        {
            var items = await context.ExpenditureDocket.Where(i => 
                        i.StockDocketId == stockDocketId
                        && i.IsDeleted == false
                    ).ToListAsync();
            return items;
        }
        public async Task<int> Add(ExpenditureDocket _model)
        {
            _model.CreatedUser = scopeContext.UserCode;
            _model.CreatedDate = DateTime.Now;
            context.ExpenditureDocket.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(ExpenditureDocket _model)
        {
            _model.UpdatedUser = scopeContext.UserCode;
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id)
        {
            ExpenditureDocket item = await context.ExpenditureDocket.Where(i => i.Id == _id).FirstOrDefaultAsync();
            if (item == default(ExpenditureDocket)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            await context.SaveChangesAsync();
            return true;
        }
    }
}

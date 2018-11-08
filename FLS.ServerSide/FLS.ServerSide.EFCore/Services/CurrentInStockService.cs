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
    public class CurrentInStockService : ICurrentInStockService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        public CurrentInStockService(FLSDbContext _context, IScopeContext _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
        }
        public async Task<List<CurrentInStock>> GetList(int warehouseId, int productId = 0)
        {
            if(warehouseId <= 0)
            {
                scopeContext.AddError("Lỗi dữ kiệu đầu vào");
                return null;
            }
            var items = await context.CurrentInStock.Where(i => 
                        i.WarehouseId == warehouseId
                        && (productId == 0 || i.ProductId == productId)
                    ).ToListAsync();
            return items;
        }
        public async Task<CurrentInStock> GetDetail(int _id)
        {
            var item = await context.CurrentInStock.FirstOrDefaultAsync(x => x.Id == _id);
            return item;
        }
        public async Task<int> Add(CurrentInStock _model)
        {
            context.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(CurrentInStock _model)
        {
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

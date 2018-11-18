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
    public class ProductUnitProductService : IProductUnitProductService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        public ProductUnitProductService(FLSDbContext _context, IScopeContext _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
        }
        public async Task<PagedList<ProductUnitProduct>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            var items = await context.ProductUnitProduct.Where(i => 
                        i.IsDeleted == false
                       // && (_model.Key == null || i.Name.Contains(_model.Key))
                    ).OrderByDescending(i => i.UpdatedDate.HasValue ? i.UpdatedDate : i.CreatedDate).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<ProductUnitProduct> GetDetail(int _id)
        {
            var item = await context.ProductUnitProduct.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<int> Add(ProductUnitProduct _model)
        {
            _model.CreatedUser = scopeContext.UserCode;
            _model.CreatedDate = DateTime.Now;
            context.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(ProductUnitProduct _model)
        {
            _model.UpdatedUser = scopeContext.UserCode;
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id)
        {
            ProductUnitProduct item = await context.ProductUnitProduct.Where(i => i.Id == _id).FirstOrDefaultAsync();
            if (item == default(ProductUnitProduct)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<ProductUnitProduct>> GetCache()
        {
            var items = await context.ProductUnitProduct.Where(i => i.IsDeleted == false).ToListAsync();
            return items;
        }
    }
}

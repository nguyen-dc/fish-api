using FLS.ServerSide.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FLS.ServerSide.SharingObject;

namespace FLS.ServerSide.EFCore.Services
{
    public class ProductGroupService : IProductGroupService
    {
        private static FLSDbContext context;
        public ProductGroupService(FLSDbContext _context)
        {
            context = _context;
        }
        public async Task<PagedList<ProductGroup>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            var items = await context.ProductGroup.Where(i => 
                        i.IsDeleted == false
                        &&(_model.Key == null || i.Name.Contains(_model.Key))
                    ).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<ProductGroup> GetDetail(int _id)
        {
            var item = await context.ProductGroup.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<int> Add(ProductGroup _model, bool _isSaveChange = true)
        {
            _model.CreatedUser = "admin";
            _model.CreatedDate = DateTime.Now;
            await context.AddAsync(_model);
            if (_isSaveChange) await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(ProductGroup _model, bool _isSaveChange = true)
        {
            _model.UpdatedUser = "admin";
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            if (_isSaveChange) await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id, bool _isSaveChange = true)
        {
            ProductGroup item = await context.ProductGroup.Where(i => i.Id == _id && i.IsDeleted == true).FirstOrDefaultAsync();
            if (item == default(ProductGroup)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            if (_isSaveChange) await context.SaveChangesAsync();
            return true;
        }
        public async Task<PagedList<ProductSubgroup>> GetSubgroups(int _groupId, PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            var items = await context.ProductSubgroup.Where(i => 
                        i.ProductGroupId == _groupId 
                        && i.IsDeleted == false
                        && (_model.Key == null || i.Name.Contains(_model.Key))
                    ).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<PagedList<Product>> GetProducts(int _groupId, PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            var items = await context.Product.Where(i =>
                        i.ProductGroupId == _groupId
                        && i.IsDeleted == false
                        && (_model.Key == null || i.Name.Contains(_model.Key))
                    ).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<List<ProductGroup>> GetCache()
        {
            var items = await context.ProductGroup.Where(i => i.IsDeleted == false).ToListAsync();
            return items;
        }
    }
}

﻿using FLS.ServerSide.EFCore.Entities;
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
    public class ProductService : IProductService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        public ProductService(FLSDbContext _context, IScopeContext _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
        }
        public async Task<PagedList<Product>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            int filter = 0;
            if (_model.Filters != null && _model.Filters.Count > 0 && _model.Filters[0].Key == FilterEnum.ProductGroup)
            {
                int.TryParse(_model.Filters[0].Value + "", out filter);
            }
            var items = await context.Product.Where(i => 
                        i.IsDeleted == false
                        && (_model.Key == null || i.Name.Contains(_model.Key))
                        && (filter == 0 || i.ProductGroupId == filter)
                    ).OrderByDescending(i => i.UpdatedDate.HasValue ? i.UpdatedDate : i.CreatedDate).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<Product> GetDetail(int _id)
        {
            var item = await context.Product.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<int> Add(Product _model)
        {
            _model.CreatedUser = scopeContext.UserCode;
            _model.CreatedDate = DateTime.Now;
            context.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(Product _model)
        {
            _model.UpdatedUser = scopeContext.UserCode;
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id)
        {
            Product item = await context.Product.Where(i => i.Id == _id).FirstOrDefaultAsync();
            if (item == default(Product)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            await context.SaveChangesAsync();
            return true;
        }
    }
}

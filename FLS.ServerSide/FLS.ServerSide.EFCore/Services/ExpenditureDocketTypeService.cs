﻿using FLS.ServerSide.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FLS.ServerSide.SharingObject;

namespace FLS.ServerSide.EFCore.Services
{
    public class ExpenditureDocketTypeService : IExpenditureDocketTypeService
    {
        private static FLSDbContext context;
        public ExpenditureDocketTypeService(FLSDbContext _context)
        {
            context = _context;
        }
        public async Task<PagedList<ExpenditureDocketType>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            var items = await context.ExpenditureDocketType.Where(i => 
                        i.IsDeleted == false
                        &&(_model.Key == null || i.Name.Contains(_model.Key))
                    ).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<ExpenditureDocketType> GetDetail(int _id)
        {
            var item = await context.ExpenditureDocketType.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<int> Add(ExpenditureDocketType _model, bool _isSaveChange = true)
        {
            _model.CreatedUser = "admin";
            _model.CreatedDate = DateTime.Now;
            await context.AddAsync(_model);
            if (_isSaveChange) await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(ExpenditureDocketType _model, bool _isSaveChange = true)
        {
            _model.UpdatedUser = "admin";
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            if (_isSaveChange) await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id, bool _isSaveChange = true)
        {
            ExpenditureDocketType item = await context.ExpenditureDocketType.Where(i => i.Id == _id && i.IsDeleted == true).FirstOrDefaultAsync();
            if (item == default(ExpenditureDocketType)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            if (_isSaveChange) await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<ExpenditureDocketType>> GetCache()
        {
            var items = await context.ExpenditureDocketType.Where(i => i.IsDeleted == false).ToListAsync();
            return items;
        }
    }
}
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
    public class ExpenditureDocketDetailService : IExpenditureDocketDetailService
    {
        private static FLSDbContext context;
        public ExpenditureDocketDetailService(FLSDbContext _context)
        {
            context = _context;
        }
        public async Task<PagedList<ExpenditureDocketDetail>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            var items = await context.ExpenditureDocketDetail.Where(i => 
                        i.IsDeleted == false
                        //&&(_model.Key == null || i.name.Contains(_model.Key))
                    ).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<ExpenditureDocketDetail> GetDetail(int _id)
        {
            var item = await context.ExpenditureDocketDetail.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<int> Add(ExpenditureDocketDetail _model)
        {
            _model.CreatedUser = "admin";
            _model.CreatedDate = DateTime.Now;
            context.ExpenditureDocketDetail.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(ExpenditureDocketDetail _model)
        {
            _model.UpdatedUser = "admin";
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id)
        {
            ExpenditureDocketDetail item = await context.ExpenditureDocketDetail.Where(i => i.Id == _id && i.IsDeleted == true).FirstOrDefaultAsync();
            if (item == default(ExpenditureDocketDetail)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            await context.SaveChangesAsync();
            return true;
        }
    }
}

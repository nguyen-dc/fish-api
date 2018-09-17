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
    public class ExpenditureDocketService : IExpenditureDocketService
    {
        private static FLSDbContext context;
        public ExpenditureDocketService(FLSDbContext _context)
        {
            context = _context;
        }
        public async Task<PagedList<ExpenditureDocket>> GetList(PageFilterModel _model)
        {
            _model.Key = string.IsNullOrWhiteSpace(_model.Key) ? null : _model.Key.Trim();
            var items = await context.ExpenditureDocket.Where(i => 
                        i.IsDeleted == false
                        &&(_model.Key == null || i.PartnerName.Contains(_model.Key))
                    ).GetPagedList(_model.Page, _model.PageSize);
            return items;
        }
        public async Task<ExpenditureDocket> GetDetail(int _id)
        {
            var item = await context.ExpenditureDocket.FirstOrDefaultAsync(x => x.Id == _id && x.IsDeleted == false);
            return item;
        }
        public async Task<int> Add(ExpenditureDocket _model, bool _isSaveChange = true)
        {
            _model.CreatedUser = "admin";
            _model.CreatedDate = DateTime.Now;
            context.ExpenditureDocket.Add(_model);
            if (_isSaveChange) await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(ExpenditureDocket _model, bool _isSaveChange = true)
        {
            _model.UpdatedUser = "admin";
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            if (_isSaveChange) await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int _id, bool _isSaveChange = true)
        {
            ExpenditureDocket item = await context.ExpenditureDocket.Where(i => i.Id == _id && i.IsDeleted == true).FirstOrDefaultAsync();
            if (item == default(ExpenditureDocket)) return false;
            item.IsDeleted = true;
            context.Entry(item).Property(x => x.IsDeleted).IsModified = true;
            if (_isSaveChange) await context.SaveChangesAsync();
            return true;
        }
    }
}

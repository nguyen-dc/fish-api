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
    public class LivestockHistoryDetailService : ILivestockHistoryDetailService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        public LivestockHistoryDetailService(FLSDbContext _context, IScopeContext _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
        }
        public async Task<LivestockHistoryDetail> GetDetail(int _id)
        {
            var item = await context.LivestockHistoryDetail.FirstOrDefaultAsync(x => x.Id == _id);
            return item;
        }
        public async Task<int> Add(LivestockHistoryDetail _model)
        {
            _model.CreatedUser = scopeContext.UserCode;
            _model.CreatedDate = DateTime.Now;
            context.Add(_model);
            await context.SaveChangesAsync();
            return _model.Id;
        }
        public async Task<bool> Modify(LivestockHistoryDetail _model)
        {
            _model.UpdatedUser = scopeContext.UserCode;
            _model.UpdatedDate = DateTime.Now;
            context.Update(_model);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

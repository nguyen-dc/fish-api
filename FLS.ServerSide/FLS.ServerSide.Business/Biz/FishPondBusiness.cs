using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.Model.Scope;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class FishPondBusiness : IFishPondBusiness
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        private readonly IFishPondService svcFishPond;
        private readonly IWarehouseService svcWarehouse;
        private readonly IMapper iMapper;
        private const string FISHPONDTYPE_WAREHOUSE_PREFIX = "[Auto] Kho ";
        private const int DEFAULT_FISHPONDTYPE_WAREHOUSE_TYPE = 0;
        public FishPondBusiness(
            FLSDbContext _context,
            IScopeContext _scopeContext, 
            IFishPondService _svcFishPond,
            IWarehouseService _svcWarehouse,
            IMapper _iMapper
            )
        {
            context = _context;
            scopeContext = _scopeContext;
            svcFishPond = _svcFishPond;
            svcWarehouse = _svcWarehouse;
            iMapper = _iMapper;
        }
        public async Task<PagedList<FishPondModel>> GetList(PageFilterModel _model)
        {
            return await svcFishPond.GetList(_model);
        }
        public async Task<FishPondModel> GetDetail(int _id)
        {
            return iMapper.Map<FishPondModel>(await svcFishPond.GetDetail(_id));
        }
        public async Task<int> Add(FishPondModel _model)
        {
            if(_model.DefaultWarehouseId.GetValueOrDefault(0) == 0)
            {
                scopeContext.AddError("Chưa chọn kho");
                return 0;
            }
            if (_model.FarmRegionId == 0)
            {
                scopeContext.AddError("Chưa chọn vùng nuôi");
                return 0;
            }
            using (var transaction = context.Database.BeginTransaction())
            {
                // create new fish-pond-type warehouse
                Warehouse warehouse = new Warehouse()
                {
                    DefaultWarehouseId = _model.DefaultWarehouseId.Value,
                    FarmRegionId = _model.FarmRegionId,
                    Name = FISHPONDTYPE_WAREHOUSE_PREFIX + _model.Name,
                    WarehouseTypeId = DEFAULT_FISHPONDTYPE_WAREHOUSE_TYPE
                };
                var warehouseId = await svcWarehouse.Add(warehouse);
                FishPond entity = iMapper.Map<FishPond>(_model);
                entity.WarehouseId = warehouseId;
                entity.Id = await svcFishPond.Add(entity);
                transaction.Commit();
                return entity.Id;
            }
        }
        public async Task<bool> Modify(int _id, FishPondModel _model)
        {
            FishPond entity = await svcFishPond.GetDetail(_id);
            if (entity == null) return false;
            Warehouse warehouse = await svcWarehouse.GetDetail(entity.WarehouseId.GetValueOrDefault(0));
            if(warehouse != null && warehouse.DefaultWarehouseId != _model.DefaultWarehouseId)
            {
                warehouse.DefaultWarehouseId = _model.DefaultWarehouseId.Value;
                if (!await svcWarehouse.Modify(warehouse))
                {
                    scopeContext.AddError("Có lỗi khi cập nhật kho mặc định.");
                    return false;
                }
            }
            entity = iMapper.Map(_model, entity);
            return await svcFishPond.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcFishPond.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcFishPond.GetCache());
        }
    }
}

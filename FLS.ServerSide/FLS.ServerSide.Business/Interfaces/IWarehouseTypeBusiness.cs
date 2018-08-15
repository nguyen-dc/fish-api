using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IWarehouseTypeBusiness
    {
        Task<PagedList<WarehouseTypeModel>> GetList(PageFilterModel _model);
        Task<WarehouseTypeModel> GetDetail(int _id);
        Task<int> Add(WarehouseTypeModel _model);
        Task<bool> Modify(int _id, WarehouseTypeModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}

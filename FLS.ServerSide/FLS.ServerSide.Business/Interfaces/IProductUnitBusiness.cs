using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IProductUnitBusiness
    {
        Task<PagedList<ProductUnitModel>> GetList(PageFilterModel _model);
        Task<ProductUnitModel> GetDetail(int _id);
        Task<int> Add(ProductUnitModel _model);
        Task<bool> Modify(int _id, ProductUnitModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}

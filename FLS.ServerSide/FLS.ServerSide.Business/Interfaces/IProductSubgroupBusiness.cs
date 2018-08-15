using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IProductSubgroupBusiness
    {
        Task<PagedList<ProductSubgroupModel>> GetList(PageFilterModel _model);
        Task<ProductSubgroupModel> GetDetail(int _id);
        Task<int> Add(ProductSubgroupModel _model);
        Task<bool> Modify(int _id, ProductSubgroupModel _model);
        Task<bool> Remove(int _id);
        Task<PagedList<ProductModel>> GetProducts(int _subgroupId, PageFilterModel _model);
        Task<List<IdNameModel>> GetCache();
    }
}

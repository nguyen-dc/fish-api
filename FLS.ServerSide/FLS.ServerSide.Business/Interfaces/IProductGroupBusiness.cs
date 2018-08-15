using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IProductGroupBusiness
    {
        Task<PagedList<ProductGroupModel>> GetList(PageFilterModel _model);
        Task<ProductGroupModel> GetDetail(int _id);
        Task<int> Add(ProductGroupModel _model);
        Task<bool> Modify(int _id, ProductGroupModel _model);
        Task<bool> Remove(int _id);
        Task<PagedList<ProductSubgroupModel>> GetSubgroups(int _groupId, PageFilterModel _model);
        Task<PagedList<ProductModel>> GetProducts(int _groupId, PageFilterModel _model);
        Task<List<IdNameModel>> GetCache();
    }
}

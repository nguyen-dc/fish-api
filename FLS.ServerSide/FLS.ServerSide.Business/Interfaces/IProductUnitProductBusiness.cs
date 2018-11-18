using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IProductUnitProductBusiness
    {
        Task<PagedList<ProductUnitProductModel>> GetList(PageFilterModel _model);
        Task<ProductUnitProductModel> GetDetail(int _id);
        Task<int> Add(ProductUnitProductModel _model);
        Task<bool> Modify(int _id, ProductUnitProductModel _model);
        Task<bool> Remove(int _id);
    }
}

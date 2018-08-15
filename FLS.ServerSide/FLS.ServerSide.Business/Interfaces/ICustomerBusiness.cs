using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface ICustomerBusiness
    {
        Task<PagedList<CustomerModel>> GetList(PageFilterModel _model);
        Task<CustomerModel> GetDetail(int _id);
        Task<int> Add(CustomerModel _model);
        Task<bool> Modify(int _id, CustomerModel _model);
        Task<bool> Remove(int _id);
    }
}

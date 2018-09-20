using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface ICustomerService
    {
        Task<PagedList<Customer>> GetList(PageFilterModel _model);
        Task<Customer> GetDetail(int _id);
        Task<int> Add(Customer _model);
        Task<bool> Modify(Customer _model);
        Task<bool> Remove(int _id);
    }
}

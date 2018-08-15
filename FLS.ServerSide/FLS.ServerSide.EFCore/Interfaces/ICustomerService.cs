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
        Task<int> Add(Customer _model, bool _isSaveChange = true);
        Task<bool> Modify(Customer _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
    }
}

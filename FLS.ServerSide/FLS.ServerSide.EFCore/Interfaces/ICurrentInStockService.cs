using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface ICurrentInStockService
    {
        Task<List<CurrentInStock>> GetList(int warehouseId, int productId);
        Task<CurrentInStock> GetDetail(int _id);
        Task<int> Add(CurrentInStock _model);
        Task<bool> Modify(CurrentInStock _model);
    }
}

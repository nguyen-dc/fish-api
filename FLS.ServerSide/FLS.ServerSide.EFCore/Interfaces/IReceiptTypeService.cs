using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IReceiptTypeService
    {
        Task<PagedList<ReceiptType>> GetList(PageFilterModel _model);
        Task<ReceiptType> GetDetail(int _id);
        Task<int> Add(ReceiptType _model, bool _isSaveChange = true);
        Task<bool> Modify(ReceiptType _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
        Task<List<ReceiptType>> GetCache();
    }
}

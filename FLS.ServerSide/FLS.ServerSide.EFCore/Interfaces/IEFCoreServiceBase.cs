using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IEFCoreServiceBase
    {
        IQueryable<T> CallStored<T>(string _storedName, object _paramList) where T : class;
    }
}

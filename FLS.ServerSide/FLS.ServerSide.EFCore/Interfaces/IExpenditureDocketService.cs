﻿using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IExpenditureDocketService
    {
        Task<PagedList<ExpenditureDocket>> GetList(PageFilterModel _model);
        Task<List<ExpenditureDocket>> GetByStockDocketId(int stockDocketId);
        Task<ExpenditureDocket> GetDetail(int _id);
        Task<int> Add(ExpenditureDocket _model);
        Task<bool> Modify(ExpenditureDocket _model);
        Task<bool> Remove(int _id);
    }
}

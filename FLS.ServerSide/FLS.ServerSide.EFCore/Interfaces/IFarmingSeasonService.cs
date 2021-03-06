﻿using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IFarmingSeasonService
    {
        Task<PagedList<FarmingSeason>> GetList(PageFilterModel _model);
        Task<FarmingSeason> GetDetail(int _id);
        Task<FarmingSeason> GetByFishPondId(int _fishPondId, DateTime? date = null);
        Task<int> Add(FarmingSeason _model);
        Task<bool> Modify(FarmingSeason _model);
        Task<bool> Remove(int _id);
    }
}

using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class SupplierBranchBusiness : ISupplierBranchBusiness
    {
        private readonly ISupplierBranchService svcSupplierBranch;
        private readonly IMapper iMapper;
        public SupplierBranchBusiness(ISupplierBranchService _svcSupplierBranch, IMapper _iMapper)
        {
            svcSupplierBranch = _svcSupplierBranch;
            iMapper = _iMapper;
        }
        public async Task<PagedList<SupplierBranchModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<SupplierBranchModel>>(await svcSupplierBranch.GetList(_model));
        }
        public async Task<SupplierBranchModel> GetDetail(int _id)
        {
            return iMapper.Map<SupplierBranchModel>(await svcSupplierBranch.GetDetail(_id));
        }
        public async Task<int> Add(SupplierBranchModel _model)
        {
            SupplierBranch entity = iMapper.Map<SupplierBranch>(_model);
            return await svcSupplierBranch.Add(entity);
        }
        public async Task<bool> Modify(int _id, SupplierBranchModel _model)
        {
            SupplierBranch entity = await svcSupplierBranch.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcSupplierBranch.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcSupplierBranch.Remove(_id);
        }
    }
}

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
    public class SupplierBusiness : ISupplierBusiness
    {
        private readonly ISupplierService svcSupplier;
        private readonly IMapper iMapper;
        public SupplierBusiness(ISupplierService _svcSupplier, IMapper _iMapper)
        {
            svcSupplier = _svcSupplier;
            iMapper = _iMapper;
        }
        public async Task<PagedList<SupplierModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<SupplierModel>>(await svcSupplier.GetList(_model));
        }
        public async Task<SupplierModel> GetDetail(int _id)
        {
            return iMapper.Map<SupplierModel>(await svcSupplier.GetDetail(_id));
        }
        public async Task<int> Add(SupplierModel _model)
        {
            Supplier entity = iMapper.Map<Supplier>(_model);
            return await svcSupplier.Add(entity);
        }
        public async Task<bool> Modify(int _id, SupplierModel _model)
        {
            Supplier entity = await svcSupplier.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcSupplier.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcSupplier.Remove(_id);
        }
        public async Task<PagedList<SupplierBranchModel>> GetBranchs(int _branchId, PageFilterModel _model)
        {
            return iMapper.Map<PagedList<SupplierBranchModel>>(await svcSupplier.GetBranchs(_branchId, _model));
        }
    }
}

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
    public class TaxPercentBusiness : ITaxPercentBusiness
    {
        private readonly ITaxPercentService svcTaxPercent;
        private readonly IMapper iMapper;
        public TaxPercentBusiness(ITaxPercentService _svcTaxPercent, IMapper _iMapper)
        {
            svcTaxPercent = _svcTaxPercent;
            iMapper = _iMapper;
        }
        public async Task<PagedList<TaxPercentModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<TaxPercentModel>>(await svcTaxPercent.GetList(_model));
        }
        public async Task<TaxPercentModel> GetDetail(int _id)
        {
            return iMapper.Map<TaxPercentModel>(await svcTaxPercent.GetDetail(_id));
        }
        public async Task<int> Add(TaxPercentModel _model)
        {
            TaxPercent entity = iMapper.Map<TaxPercent>(_model);
            return await svcTaxPercent.Add(entity);
        }
        public async Task<bool> Modify(int _id, TaxPercentModel _model)
        {
            TaxPercent entity = await svcTaxPercent.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcTaxPercent.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcTaxPercent.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcTaxPercent.GetCache());
        }
    }
}

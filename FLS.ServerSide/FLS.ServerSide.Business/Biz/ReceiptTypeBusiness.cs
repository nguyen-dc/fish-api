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
    public class ReceiptTypeBusiness : IReceiptTypeBusiness
    {
        private readonly IReceiptTypeService svcReceiptType;
        private readonly IMapper iMapper;
        public ReceiptTypeBusiness(IReceiptTypeService _svcReceiptType, IMapper _iMapper)
        {
            svcReceiptType = _svcReceiptType;
            iMapper = _iMapper;
        }
        public async Task<PagedList<ExpenditureTypeModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ExpenditureTypeModel>>(await svcReceiptType.GetList(_model));
        }
        public async Task<ExpenditureTypeModel> GetDetail(int _id)
        {
            return iMapper.Map<ExpenditureTypeModel>(await svcReceiptType.GetDetail(_id));
        }
        public async Task<int> Add(ExpenditureTypeModel _model)
        {
            ReceiptType entity = iMapper.Map<ReceiptType>(_model);
            return await svcReceiptType.Add(entity);
        }
        public async Task<bool> Modify(int _id, ExpenditureTypeModel _model)
        {
            ReceiptType entity = await svcReceiptType.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcReceiptType.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcReceiptType.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcReceiptType.GetCache());
        }
    }
}

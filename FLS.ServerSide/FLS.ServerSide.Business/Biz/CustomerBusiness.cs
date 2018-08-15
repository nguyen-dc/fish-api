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
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly ICustomerService svcCustomer;
        private readonly IMapper iMapper;
        public CustomerBusiness(ICustomerService _svcCustomer, IMapper _iMapper)
        {
            svcCustomer = _svcCustomer;
            iMapper = _iMapper;
        }
        public async Task<PagedList<CustomerModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<CustomerModel>>(await svcCustomer.GetList(_model));
        }
        public async Task<CustomerModel> GetDetail(int _id)
        {
            return iMapper.Map<CustomerModel>(await svcCustomer.GetDetail(_id));
        }
        public async Task<int> Add(CustomerModel _model)
        {
            Customer entity = iMapper.Map<Customer>(_model);
            return await svcCustomer.Add(entity);
        }
        public async Task<bool> Modify(int _id, CustomerModel _model)
        {
            Customer entity = await svcCustomer.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcCustomer.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcCustomer.Remove(_id);
        }
    }
}

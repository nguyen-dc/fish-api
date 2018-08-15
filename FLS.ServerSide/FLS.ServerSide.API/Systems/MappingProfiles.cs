using AutoMapper;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLS.ServerSide.API.Systems
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Product
            CreateMap<Product, ProductDetailModel>().ReverseMap();
            // Product Group
            CreateMap<ProductGroup, ProductGroupModel>().ReverseMap();
        }
    }
}

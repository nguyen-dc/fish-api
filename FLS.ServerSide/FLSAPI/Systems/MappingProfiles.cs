using AutoMapper;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLS.ServerSide.API.Systems
{
    public partial class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // ************ MAPS MODEL & ENTITIES ********************************* //
            CreateMap<CustomerModel, Customer>(MemberList.Source).ReverseMap();
            CreateMap<ExpenditureDocketModel, ExpenditureDocket>(MemberList.Source).ReverseMap();
            CreateMap<ExpenditureDocketDetailModel, ExpenditureDocketDetail>(MemberList.Source).ReverseMap();
            CreateMap<ExpenditureDocketTypeModel, ExpenditureDocketType>(MemberList.Source).ReverseMap();
            CreateMap<FarmingSeasonModel, FarmingSeason>(MemberList.Source).ReverseMap();
            CreateMap<FarmRegionModel, FarmRegion>(MemberList.Source).ReverseMap();
            CreateMap<FishPondModel, FishPond>(MemberList.Source).ReverseMap();
            CreateMap<ProductGroupModel, ProductGroup>(MemberList.Source).ReverseMap();
            CreateMap<ProductModel, Product>(MemberList.Source).ReverseMap();
            CreateMap<ProductSubgroupModel, ProductSubgroup>(MemberList.Source).ReverseMap();
            CreateMap<ProductUnitModel, ProductUnit>(MemberList.Source).ReverseMap();
            CreateMap<StockIssueDocketModel, StockIssueDocket>(MemberList.Source).ReverseMap();
            CreateMap<StockIssueDocketDetailModel, StockIssueDocketDetail>(MemberList.Source).ReverseMap();
            CreateMap<StockIssueDocketTypeModel, StockIssueDocketType>(MemberList.Source).ReverseMap();
            CreateMap<StockReceiveDocketModel, StockReceiveDocket>(MemberList.Source).ReverseMap();
            CreateMap<StockReceiveDocketDetailModel, StockReceiveDocketDetail>(MemberList.Source).ReverseMap();
            CreateMap<StockReceiveDocketTypeModel, StockReceiveDocketType>(MemberList.Source).ReverseMap();
            CreateMap<SupplierModel, Supplier>(MemberList.Source).ReverseMap();
            CreateMap<SupplierBranchModel, SupplierBranch>(MemberList.Source).ReverseMap();
            CreateMap<TaxPercentModel, TaxPercent>(MemberList.Source).ReverseMap();
            CreateMap<WarehouseModel, Warehouse>(MemberList.Source).ReverseMap();
            CreateMap<WarehouseTypeModel, WarehouseType>(MemberList.Source).ReverseMap();
            // ******************************************************************** //

            // ************ MAPS ENTITIES TO CACHE ******************************** //
            CreateMap<ExpenditureDocketType, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.check, m => m.MapFrom(s => s.IsReceipt))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Description));
            CreateMap<FarmingSeason, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Name));
            CreateMap<FarmRegion, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Name));
            CreateMap<FishPond, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Name));
            CreateMap<ProductGroup, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Description));
            CreateMap<ProductSubgroup, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.parentId, m => m.MapFrom(s => s.ProductGroupId))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Description));
            CreateMap<ProductUnit, IdNameModel>(MemberList.Destination)
               .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
               .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
               .ForMember(d => d.check, m => m.MapFrom(s => s.HasScale))
               .ForMember(d => d.description, m => m.MapFrom(s => s.HasScale ? "Có phần thập phân" : "Không có phần thập phân"));
            CreateMap<StockIssueDocketType, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.check, m => m.MapFrom(s => s.ReceiptNeeded))
                .ForMember(d => d.belongId, m => m.MapFrom(s => s.ReceiptTypeId))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Name));
            CreateMap<StockReceiveDocketType, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.check, m => m.MapFrom(s => s.PayslipNeeded))
                .ForMember(d => d.belongId, m => m.MapFrom(s => s.PayslipTypeId))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Name));
            CreateMap<TaxPercent, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.value, m => m.MapFrom(s => s.Value))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Name));
            CreateMap<Warehouse, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Name));
            CreateMap<WarehouseType, IdNameModel>(MemberList.Destination)
                .ForMember(d => d.id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.description, m => m.MapFrom(s => s.Name));
            // ******************************************************************** //
        }
    }
}

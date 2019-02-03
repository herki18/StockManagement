using System.Linq;
using AutoMapper;
using StockManagement.Api.Contract.Entities;
using StockManagement.Api.Contract.Models;
using StockManagement.Api.Contract.Models.Batch;

namespace StockManagement.Api.Contract
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserEntity, UserDTO>().ReverseMap();
            CreateMap<IGrouping<int, BatchEntity>, StockDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.FruitId, opt => opt.MapFrom(src => src.FirstOrDefault().Fruit.Id))
                .ForMember(dest => dest.Fruit, opt => opt.MapFrom(src => src.FirstOrDefault().Fruit.Name))
                .ForMember(dest => dest.VarietyId, opt => opt.MapFrom(src => src.FirstOrDefault().Fruit.Variety.Id))
                .ForMember(dest => dest.Variety, opt => opt.MapFrom(src => src.FirstOrDefault().Fruit.Variety.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Sum(s => s.Quantity)));

            CreateMap<BatchEntity, BatchDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Fruit, opt => opt.MapFrom(src => src.Fruit.Name))
                .ForMember(dest => dest.FruitId, opt => opt.MapFrom(src => src.FruitId))
                .ForMember(dest => dest.Variety, opt => opt.MapFrom(src => src.Fruit.Variety.Name))
                .ForMember(dest => dest.VarietyId, opt => opt.MapFrom(src => src.Fruit.VarietyId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<BatchForCreate, BatchEntity>(MemberList.None)
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FruitId, opt => opt.MapFrom(src => src.FruitId))
                .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<BatchForUpdate, BatchEntity>(MemberList.None)
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FruitId, opt => opt.MapFrom(src => src.FruitId))
                .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<FruitEntity, FruitDTO>();
            CreateMap<VarietyEntity, VarietyDTO>().ReverseMap();
        }
    }
}
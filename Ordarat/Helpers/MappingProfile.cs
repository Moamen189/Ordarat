using AutoMapper;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.DataAccessLayer.Entities.Identity;
using Ordarat.Dtos;

namespace Ordarat.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<PictureUrlResolver>());

            CreateMap<DataAccessLayer.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<CustomerBasketDto, CustomerBasket>();

            CreateMap<AddressDto, DataAccessLayer.Entities.Order_Aggregate.Address>();


        }
    }
}

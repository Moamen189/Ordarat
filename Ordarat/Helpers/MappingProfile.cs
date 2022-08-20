using AutoMapper;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.DataAccessLayer.Entities.Identity;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
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

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DelivaryMethod, O => O.MapFrom(S => S.DelivaryMethod.ShortName))
                .ForMember(d => d.DeliveryCost, O => O.MapFrom(S => S.DelivaryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.ItemOrder.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.ItemOrder.ProductName))

                .ForMember(D => D.PictureUrl, O => O.MapFrom(S => S.ItemOrder.PictureUrl))
                .ForMember(D => D.PictureUrl, O => O.MapFrom <OrderItemUrlResolver>());






        }
    }
}

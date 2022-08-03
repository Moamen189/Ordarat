﻿using AutoMapper;
using Ordarat.DataAccessLayer.Entities;
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



        }
    }
}
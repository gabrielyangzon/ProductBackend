﻿using AutoMapper;
using Product.DataTypes.Models;

namespace Product.Api.Mapper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<ProductModelAddDto, ProductModel>();

            CreateMap<ProductModelEditDto, ProductModel>(); 
        }
    }
}

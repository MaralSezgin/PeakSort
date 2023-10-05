using AutoMapper;
using PeakSort.Entities.Concrete;
using PeakSort.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Business.AutoMapper.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAddDto, Product>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));//ProductAddDto i product a çevirdik ve tarihini şimiki zamanı atadık
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}

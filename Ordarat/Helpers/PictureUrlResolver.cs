using AutoMapper;
using Microsoft.Extensions.Configuration;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.Dtos;

namespace Ordarat.Helpers
{
    public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(destMember))
                return $"{configuration["ApiUrl"]}{source.PictureUrl}";
            return null;     
        }
    }
}

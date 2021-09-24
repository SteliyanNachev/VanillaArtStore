
namespace VanillaArtStore.Infrastructure
{
    using AutoMapper;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Models.Products;
    using VanillaArtStore.Services.Products.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, ProductListingViewModel>();
            this.CreateMap<Product, ProductServiceModel>();
            this.CreateMap<ProductServiceModel, ProductFormModel>();

        }
    }
}

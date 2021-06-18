using ACME.Products.Categories;
using ACME.Products.Products;
using AutoMapper;
using System.IO;
using System.Linq;

namespace ACME.Products.Web
{
    public class ProductsWebAutoMapperProfile : Profile
    {
        public ProductsWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<CategoryDto, CreateUpdateCategoryDto>();
            CreateMap<ProductDto, CreateUpdateProductDto>();

            // ADD a NEW MAPPING
            //Categories
            CreateMap<Pages.Categories.CreateModalModel.CreateCategoryViewModel, Categories.CreateUpdateCategoryDto>().ForMember(a => a.Picture,opts=> opts.Ignore());
            CreateMap<Pages.Categories.EditModalModel.EditCategoryViewModel, Categories.CreateUpdateCategoryDto>().ForMember(a => a.Picture, opts => opts.Ignore());
            CreateMap<CategoryDto,Pages.Categories.EditModalModel.EditCategoryViewModel>() ;

            //Products
            CreateMap<Pages.Products.CreateModalModel.CreateProductViewModel, Products.CreateUpdateProductDto>().ForMember(a => a.Picture, opts => opts.Ignore());
            CreateMap<Pages.Products.EditModalModel.EditProductViewModel, Products.CreateUpdateProductDto>().ForMember(a => a.Picture, opts => opts.Ignore());
            CreateMap<ProductDto, Pages.Products.EditModalModel.EditProductViewModel>().ForMember(des => des.CategoryIds, opts => opts.MapFrom(z => z.Categories.Select(a=>a.Id))) ;
        }
    }
}

using ACME.Products.Categories;
using ACME.Products.Common;
using ACME.Products.ProductCategories;
using ACME.Products.Products;
using AutoMapper;
using System;
using System.IO;
using System.Linq;

namespace ACME.Products
{
    public class ProductsApplicationAutoMapperProfile : Profile
    {
        public ProductsApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Product, ProductDto>()
                .ForMember(a => a.Categories, (opts) => opts.MapFrom(src => src.ProductCategories.Select(a => a.Category)))
                .ForMember(a => a.Picture, (opts) => opts.MapFrom(src => src.Picture != null ? Convert.ToBase64String(src.Picture) : ""));
            CreateMap<ProductCategory, CategoryDto>()
               .ForMember(a => a.Picture, (opts) => opts.MapFrom(src => src.Category.Picture))
               .ForMember(a => a.Id, (opts) => opts.MapFrom(src => src.Category.Id))
               .ForMember(a => a.CategoryName, (opts) => opts.MapFrom(src => src.Category.CategoryName))
               .ForMember(a => a.Description, (opts) => opts.MapFrom(src => src.Category.Description))
               .ForMember(a => a.ParentCategoryId, (opts) => opts.MapFrom(src => src.Category.ParentCategoryId));
            CreateMap<CreateUpdateProductDto, Product>().ForMember(a=>a.ProductCategories,opts=> opts.Ignore());


            CreateMap<Category, LookupDto<int>>().ForMember(des => des.Name, opt => opt.MapFrom(z => z.CategoryName));
            CreateMap<Category, CategoryDto>()
                .ForMember(a => a.Picture, (opts) => opts.MapFrom(src => src.Picture != null ? Convert.ToBase64String(src.Picture) : ""));
           
            CreateMap<CreateUpdateCategoryDto, Category>();
        }
    }
}

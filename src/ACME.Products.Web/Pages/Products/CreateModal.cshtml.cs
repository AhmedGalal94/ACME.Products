using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACME.Products.Categories;
using ACME.Products.Common;
using ACME.Products.Products;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UoN.ExpressiveAnnotations.NetCore.Attributes;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ACME.Products.Web.Pages.Products
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty]
        public CreateProductViewModel Product { get; set; }

        public MultiSelectList Categories { get; set; }

        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;

        public CreateModalModel(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        public async Task OnGetAsync()
        {
            Product = new CreateProductViewModel();
            var categoriesLookups = await _categoriesService.GetCategoriesLookups();
            Categories = new MultiSelectList(categoriesLookups, nameof(LookupDto<int>.Id), nameof(LookupDto<int>.Name), Product.CategoryIds);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductViewModel, CreateUpdateProductDto>(Product);
            if (Product.Picture != null && Product.Picture.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    Product.Picture.CopyTo(stream);
                    dto.Picture = stream.ToArray();
                }
            }
            await _productsService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateProductViewModel
        {

            [Required]
            [MaxLength(ACME.Products.Products.ProductsConsts.MaxNameLength)]
            public string ProductName { get; set; }

            [Required]
            [Range(0,double.MaxValue)]
            public decimal Price { get; set; }

            [MinLength(1)]
            public IEnumerable<int> CategoryIds { get; set; }

            [Required]
            [Range(0, int.MaxValue)]
            public int UnitsInStock { get; set; }

            public bool Published { get; set; }
            [DataType(DataType.Date)]
            public DateTime? PublishDate { get; set; }

            public IFormFile Picture { get; set; }
        }
    }
}


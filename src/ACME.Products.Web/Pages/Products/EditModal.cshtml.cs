using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACME.Products.Categories;
using ACME.Products.Common;
using ACME.Products.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UoN.ExpressiveAnnotations.NetCore.Attributes;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ACME.Products.Web.Pages.Products
{
    public class EditModalModel : ProductsPageModel
    {
        [BindProperty]
        public EditProductViewModel Product { get; set; }

        public MultiSelectList Categories { get; set; } = new SelectList(new List<LookupDto<int>>());

        private readonly ICategoriesService _caregoriesService;
        private readonly IProductsService _productsService;

        public EditModalModel(IProductsService productsService,ICategoriesService caregoriesService)
        {
            _caregoriesService = caregoriesService;
            _productsService = productsService;

        }
        public async Task OnGetAsync(int id)
        {
            var categoryDto = await _productsService.GetAsync(id);
            Product = ObjectMapper.Map<ProductDto, EditProductViewModel>(categoryDto);
            var CategoriesLookups = await _caregoriesService.GetCategoriesLookups();
            Categories = new MultiSelectList(CategoriesLookups, nameof(LookupDto<int>.Id), nameof(LookupDto<int>.Name), Product.CategoryIds);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditProductViewModel, CreateUpdateProductDto>(Product);
            if (Product.UpdatedPicture != null && Product.UpdatedPicture.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    Product.UpdatedPicture.CopyTo(stream);
                    dto.Picture = stream.ToArray();
                }
            }
            else if (!string.IsNullOrWhiteSpace(Product.Picture))
            {
                dto.Picture = Convert.FromBase64String(Product.Picture);
            }
            await _productsService.UpdateAsync(Product.Id, dto);
            return NoContent();
        }
        public class EditProductViewModel
        {
            [HiddenInput]
            public int Id { get; set; }

            [Required]
            [MaxLength(ACME.Products.Products.ProductsConsts.MaxNameLength)]
            public string ProductName { get; set; }

            [Required]
            [Range(0, double.MaxValue)]
            public decimal Price { get; set; }

            [MinLength(1)]
            public List<int> CategoryIds { get; set; }

            [Required]
            [Range(0, int.MaxValue)]
            public int UnitsInStock { get; set; }

            public bool Published { get; set; }

            [DataType(DataType.Date)]
            public DateTime? PublishDate { get; set; }

            [HiddenInput]
            public string Picture { get; set; }
            public IFormFile UpdatedPicture { get; set; }

        }
    }
}

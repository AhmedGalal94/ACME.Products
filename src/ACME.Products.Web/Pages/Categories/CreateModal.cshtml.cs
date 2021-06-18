using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACME.Products.Categories;
using ACME.Products.Common;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ACME.Products.Web.Pages.Categories
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty]
        public CreateCategoryViewModel Category { get; set; }

        public SelectList ParentCategories { get; set; } = new SelectList(new List<LookupDto<int>>());

        private readonly ICategoriesService _caregoriesService;

        public CreateModalModel(ICategoriesService caregoriesService)
        {
            _caregoriesService = caregoriesService;
        }

        public async Task OnGetAsync()
        {
            Category = new CreateCategoryViewModel();
            var parentCategories = await _caregoriesService.GetParentCategoriesLookups();
            var defaultOption = new LookupDto<int> { Id = 0, Name = "Parent" };
            parentCategories.Add(defaultOption);
            ParentCategories = new SelectList(parentCategories, nameof(LookupDto<int>.Id), nameof(LookupDto<int>.Name), Category.ParentCategoryId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateCategoryViewModel, ACME.Products.Categories.CreateUpdateCategoryDto>(Category);
            if (Category.Picture != null && Category.Picture.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    Category.Picture.CopyTo(stream);
                    dto.Picture = stream.ToArray();
                }
            }
            await _caregoriesService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateCategoryViewModel
        {

            [Required]
            [MaxLength(CategoirsConsts.MaxNameLength)]
            public string CategoryName { get; set; }
            public int ParentCategoryId { get; set; }
            [TextArea]
            public string Description { get; set; }
            public IFormFile Picture { get; set; }
        }
    }
}


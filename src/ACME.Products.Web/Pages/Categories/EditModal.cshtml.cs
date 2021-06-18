using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACME.Products.Categories;
using ACME.Products.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ACME.Products.Web.Pages.Categories
{
    public class EditModalModel : ProductsPageModel
    {
        [BindProperty]
        public EditCategoryViewModel Category { get; set; }

        public SelectList ParentCategories { get; set; } = new SelectList(new List<LookupDto<int>>());

        private readonly ICategoriesService _caregoriesService;

        public EditModalModel(ICategoriesService caregoriesService)
        {
            _caregoriesService = caregoriesService;

        }
        public async Task OnGetAsync(int id)
        {  
            var categoryDto = await _caregoriesService.GetAsync(id);
            Category = ObjectMapper.Map<CategoryDto, EditCategoryViewModel>(categoryDto);
            var parentCategories = await _caregoriesService.GetParentCategoriesLookups();
            var defaultOption = new LookupDto<int> { Id = 0, Name = "Parent" };
            parentCategories.Add(defaultOption);
            ParentCategories = new SelectList(parentCategories, nameof(LookupDto<int>.Id), nameof(LookupDto<int>.Name), Category.ParentCategoryId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditCategoryViewModel, ACME.Products.Categories.CreateUpdateCategoryDto>(Category);
            if (Category.UpdatedPicture  != null && Category.UpdatedPicture.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    Category.UpdatedPicture.CopyTo(stream);
                    dto.Picture = stream.ToArray();
                }
            }
            else if(!string.IsNullOrWhiteSpace(Category.Picture))
            {
                dto.Picture = Convert.FromBase64String(Category.Picture);
            }
            await _caregoriesService.UpdateAsync(Category.Id, dto);
            return NoContent();
        }
        public class EditCategoryViewModel
        {
            [HiddenInput]
            public int Id { get; set; }

            [Required]
            [MaxLength(CategoirsConsts.MaxNameLength)]
            public string CategoryName { get; set; }
            public int ParentCategoryId { get; set; }
            [TextArea]
            public string Description { get; set; }
            [HiddenInput]
            public string Picture { get; set; }

            public IFormFile UpdatedPicture { get; set; }
        }
    }
}

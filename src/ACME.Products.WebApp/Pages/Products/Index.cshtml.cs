using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACME.Products.Categories;
using ACME.Products.Common;
using ACME.Products.Localization;
using ACME.Products.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Http.Client.DynamicProxying;
using Volo.Abp.Users;

namespace ACME.Products.WebApp.Pages.Products
{
    //[Authorize(ACME.Products.Permissions.ProductsPermissions.Products.Default)]
    public class IndexModel : ProductsPageModel
    {
        private readonly IStringLocalizer<ProductsResource> _localizer;
        IProductsService _productsService;
        ICurrentUser _currentUser;
        public SelectList Categories { get; set; }
        private readonly ICategoriesService _caregoriesService;
        public IndexModel(IProductsService ProductsService, ICategoriesService caregoriesService, IStringLocalizer<ProductsResource> localizer, ICurrentUser currentUser)
        {
            _caregoriesService = caregoriesService;
            _productsService = ProductsService;
            _currentUser = currentUser;
            _localizer = localizer;
        }
        public async Task OnGetAsync()
        {
            var CategoriesLookups = await _caregoriesService.GetCategoriesLookups();
            Categories = new SelectList(CategoriesLookups, nameof(LookupDto<int>.Id), nameof(LookupDto<int>.Name),0);
        }
        public async Task<JsonResult> OnPostData([FromBody]FilteredPagedSortedResultRequestDto input)
        {
            return new JsonResult(await _productsService.GetListAsync(input));
        }
    }
}

using ACME.Products.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ACME.Products.WebApp.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class ProductsPageModel : AbpPageModel
    {
        protected ProductsPageModel()
        {
            LocalizationResourceType = typeof(ProductsResource);
        }
    }
}
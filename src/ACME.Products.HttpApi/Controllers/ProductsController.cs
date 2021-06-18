using ACME.Products.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ACME.Products.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ProductsController : AbpController
    {
        protected ProductsController()
        {
            LocalizationResource = typeof(ProductsResource);
        }
    }
}
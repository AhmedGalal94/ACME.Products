using System;
using System.Collections.Generic;
using System.Text;
using ACME.Products.Localization;
using Volo.Abp.Application.Services;

namespace ACME.Products
{
    /* Inherit your application services from this class.
     */
    public abstract class ProductsAppService : ApplicationService
    {
        protected ProductsAppService()
        {
            LocalizationResource = typeof(ProductsResource);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ACME.Products.Categories
{
    public class CategoryNameAlreadyExistsException : BusinessException
    {
        public CategoryNameAlreadyExistsException(string name)
            : base(ProductsDomainErrorCodes.CategoryNameAlreadyExist)
        {
            WithData(nameof(Category.CategoryName).ToPascalCase(), name);
        }
    }
}

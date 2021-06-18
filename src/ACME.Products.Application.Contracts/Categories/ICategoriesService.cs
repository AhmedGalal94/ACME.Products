using ACME.Products.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ACME.Products.Categories
{
    public interface ICategoriesService : ICrudAppService<
            CategoryDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateCategoryDto>
    {
        Task<List<LookupDto<int>>> GetParentCategoriesLookups();
        Task<List<LookupDto<int>>> GetCategoriesLookups();
    }
}

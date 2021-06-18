using ACME.Products.Common;
using ACME.Products.Localization;
using ACME.Products.Permissions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace ACME.Products.Categories
{
    public class CategoriesService : CrudAppService<
            Category,
            CategoryDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateCategoryDto>,
            ICategoriesService
    {

        IStringLocalizer<ProductsResource> _localizer;
        public CategoriesService(IRepository<Category, int> repository, IStringLocalizer<ProductsResource> localizer) : base(repository)
        {
            GetPolicyName    = ProductsPermissions.Categories.Default;
            GetListPolicyName= ProductsPermissions.Categories.Default;
            CreatePolicyName = ProductsPermissions.Categories.Create;
            UpdatePolicyName = ProductsPermissions.Categories.Edit;
            DeletePolicyName = ProductsPermissions.Categories.Delete;
            _localizer = localizer;
        }

        public async Task<List<LookupDto<int>>> GetParentCategoriesLookups()
        {
            var data = await Repository.GetListAsync(a => a.ParentCategoryId == 0);
            var result = ObjectMapper.Map<List<Category>,List<LookupDto<int>>>(data);
            return result;

        }
        public async Task<List<LookupDto<int>>> GetCategoriesLookups()
        {
            var data = await Repository.GetListAsync();
            var result = ObjectMapper.Map<List<Category>, List<LookupDto<int>>>(data);
            return result;

        }

        public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            var existNameRecord = await Repository.FindAsync(a => a.CategoryName == input.CategoryName);
            if (existNameRecord != null)
                throw new AbpValidationException(_localizer["CategoryNameAlreadyExist"], new [] { new ValidationResult(_localizer["CategoryNameAlreadyExist"], new [] { nameof(input.CategoryName).ToPascalCase() }) });
            return  await base.CreateAsync(input);
        }

        public override async Task<CategoryDto> UpdateAsync(int id, CreateUpdateCategoryDto input)
        {
            var existNameRecord = await Repository.FindAsync(a => a.CategoryName == input.CategoryName && a.Id != id);
            if (existNameRecord != null)
                throw new AbpValidationException(_localizer["CategoryNameAlreadyExist"], new [] { new ValidationResult(_localizer["CategoryNameAlreadyExist"], new [] { nameof(input.CategoryName).ToPascalCase() }) });


            return await base.UpdateAsync(id, input);
        }

    }
}

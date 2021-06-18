using ACME.Products.Permissions;
using ACME.Products.ProductCategories;
using ACME.Products.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ACME.Products.Products
{
    public class ProductsService : CrudAppService<
            Product,
            ProductDto,
            int,
            FilteredPagedSortedResultRequestDto,
            CreateUpdateProductDto>,IProductsService
    {
        public ProductsService(IRepository<Product, int> repository) : base(repository)
        {
            //GetPolicyName = ProductsPermissions.Products.Default;
            //GetListPolicyName = ProductsPermissions.Products.Default;
            CreatePolicyName = ProductsPermissions.Products.Create;
            UpdatePolicyName = ProductsPermissions.Products.Edit;
            DeletePolicyName = ProductsPermissions.Products.Delete;
        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            await CheckCreatePolicyAsync();
            var entity = MapToEntity(input);
            input.CategoryIds
                .Select(CategoryId => new ProductCategory
                {
                    CategoryId = CategoryId
                }).ToList()
                .ForEach(ProductCategory=> { entity.ProductCategories.Add(ProductCategory); });

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return await GetAsync(entity.Id);
        }

        public override async Task<ProductDto> GetAsync(int id)
        {
            await CheckGetPolicyAsync();
            var entity = Repository.Include(a => a.ProductCategories)
                .ThenInclude(a => a.Category)
                .FirstOrDefault(a => a.Id == id);
            var dto =MapToGetOutputDto(entity);
            return dto;
        }

        public override async Task<PagedResultDto<ProductDto>> GetListAsync(FilteredPagedSortedResultRequestDto input)
        {
            await CheckGetListPolicyAsync();
            Expression<Func<Product, bool>> filter = p => !input.CategoryId.HasValue || p.ProductCategories.Any(a =>a.CategoryId == input.CategoryId);
            var result = Repository
                                .Include(a => a.ProductCategories)
                                .ThenInclude(a => a.Category).Where(filter)
                                .OrderBy(input.Sorting)
                                .Skip(input.SkipCount)
                                .Take(input.MaxResultCount).ToList();
            var count = input.CategoryId.HasValue ? await Repository.CountAsync(filter) : await Repository.GetCountAsync();

            var data = await MapToGetListOutputDtosAsync(result);
            return new PagedResultDto<ProductDto>(count, data);
        }

        public override async Task<ProductDto> UpdateAsync(int id, CreateUpdateProductDto input)
        {
            await CheckUpdatePolicyAsync();
            var entity = Repository.Include(a => a.ProductCategories).ThenInclude(a => a.Category).FirstOrDefault(a => a.Id == id);
            MapToEntity(input, entity);
            var addedProductCategories = input.CategoryIds.Where(inputCategoryId =>
                                                           !entity.ProductCategories.Select(exist => exist.CategoryId).Contains(inputCategoryId))
                                                          .Select(CategoryId => new ProductCategory
                                                          {
                                                              CategoryId = CategoryId
                                                          }).ToList();

            entity.ProductCategories.RemoveAll(exist => !input.CategoryIds.Contains(exist.CategoryId));
            entity.ProductCategories.AddRange(addedProductCategories);
            await Repository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return await MapToGetOutputDtoAsync(entity);
        }
    }
}

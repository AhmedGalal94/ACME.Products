using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ACME.Products.Categories
{
    public class CategoryDto : EntityDto<int>
    {
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}

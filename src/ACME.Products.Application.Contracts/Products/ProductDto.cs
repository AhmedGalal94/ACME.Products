using ACME.Products.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ACME.Products.Products
{
    public class ProductDto : EntityDto<int>
    {
        public ProductDto()
        {
            Categories = new List<CategoryDto>();
        }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public bool Published { get; set; }
        public string Picture { get; set; }
        [Required]
        public int UnitsInStock { get; set; }
        public DateTime? PublishDate { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}

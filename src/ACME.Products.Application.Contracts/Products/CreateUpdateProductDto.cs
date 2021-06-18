using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace ACME.Products.Products
{
    public class CreateUpdateProductDto :  IValidatableObject
    {
        public CreateUpdateProductDto()
        {
            CategoryIds = new List<int>();
        }

        [Required]
        [MaxLength(ProductsConsts.MaxNameLength)]
        public string ProductName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [MinLength(1)]
        public IEnumerable<int> CategoryIds { get; set; }

        public bool Published { get; set; }

        public byte[] Picture { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int UnitsInStock { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Published  && !PublishDate.HasValue)
            {
                yield return new ValidationResult(
                    "publish date must be provided",
                    new[] { "PublishDate" }
                );
            }
        }
    }
}


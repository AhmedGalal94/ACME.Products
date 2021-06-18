using ACME.Products.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ACME.Products.ProductCategories
{
    public class ProductCategory : Entity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public override object[] GetKeys()
        {
            return new Object[] { CategoryId, ProductId };
        }
        public Category Category { get; set; }
    }
}

using ACME.Products.ProductCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace ACME.Products.Products
{
    public class Product : Entity<int>
    {
        public Product()
        {
            ProductCategories = new List<ProductCategory>();
        }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public bool Published { get; set; }
        public byte[] Picture { get; set; }
        public int UnitsInStock { get; set; }
        public DateTime? PublishDate { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}

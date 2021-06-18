using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ACME.Products.Categories
{
    public class Category : Entity<int>
    {
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}

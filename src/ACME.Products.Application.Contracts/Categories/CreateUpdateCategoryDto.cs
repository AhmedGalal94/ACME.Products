using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACME.Products.Categories
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [MaxLength(CategoirsConsts.MaxNameLength)]
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}

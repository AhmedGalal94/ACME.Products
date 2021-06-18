using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ACME.Products.Products
{
    public class FilteredPagedSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public int? CategoryId { get; set; }
    }
}

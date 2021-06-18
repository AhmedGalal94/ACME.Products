using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Products.Common
{
    public class LookupDto<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
    }
}

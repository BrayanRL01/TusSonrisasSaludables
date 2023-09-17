using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class VwSubCategory
    {
        public int CategoryId { get; set; }
        public string MainCategory { get; set; } = null!;
        public string SubCategory { get; set; } = null!;
    }
}

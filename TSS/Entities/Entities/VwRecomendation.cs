using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class VwRecomendation
    {
        public int RecomendationId { get; set; }
        public DateTime PostDate { get; set; }
        public string FullName { get; set; } = null!;
        public string SpecialtyName { get; set; } = null!;
        public string Information { get; set; } = null!;
        public byte[]? PostImage { get; set; }
    }
}

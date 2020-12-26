using System.Collections.Generic;

namespace NiksoftCore.ITCF.Service
{
    public class City
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ProvinceId { get; set; }
        public int CountryId { get; set; }
        public bool IsProvinceCenter { get; set; }
        public bool IsMain { get; set; }

        public virtual Province Province { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<IndustrialPark> IndustrialParks { get; set; }
    }
}
